﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Threading;
using System.Collections;
using NSvn.Core;
using ICSharpCode.Core;
using PragmaSQL.Core;

namespace PragmaSQL.Svn.Gui
{
  public partial class InfoPanel : UserControl
  {
    private IViewContent _viewContent;
    public InfoPanel( )
    {
      InitializeComponent();
    }

    public InfoPanel( IViewContent viewContent )
      : this()
    {
      this._viewContent = viewContent;
    }

    public void ShowError( Exception ex )
    {
      TextBox txt = new TextBox();
      txt.Multiline = true;
      txt.ReadOnly = true;
      txt.BackColor = SystemColors.Window;
      SvnClientException svn;
      txt.Text = "";
      while ((svn = ex as SvnClientException) != null)
      {
        txt.Text += svn.SvnError + Environment.NewLine;
        ex = svn.InnerException;
      }
      if (ex != null)
      {
        txt.Text += ex.ToString();
      }
      txt.Dock = DockStyle.Fill;
      revisionList.Controls.Add(txt);
    }

    int lastRevision = -1;
    public void AddLogMessage( LogMessage logMessage )
    {
      if (lastRevision < 0)
        lastRevision = logMessage.Revision;

      ListViewItem newItem = new ListViewItem(new string[] {
			                                        	logMessage.Revision.ToString(),
			                                        	logMessage.Author,
			                                        	logMessage.Date.ToString(),
			                                        	logMessage.Message
			                                        });
      newItem.Tag = logMessage;
      revisionList.Items.Add(newItem);
    }


    void RevisionListViewSelectionChanged( object sender, EventArgs e )
    {
      changesList.Items.Clear();
      if (revisionList.SelectedItems.Count == 0)
      {
        commentRichTextBox.Text = "";
        commentRichTextBox.Enabled = false;
        return;
      }
      commentRichTextBox.Enabled = true;
      ListViewItem item = revisionList.SelectedItems[0];
      LogMessage logMessage = item.Tag as LogMessage;
      commentRichTextBox.Text = logMessage.Message;
      ChangedPathDictionary changes = logMessage.ChangedPaths;
      if (changes == null)
      {
        changesList.Items.Add("Loading...");
        if (!isLoadingChangedPaths)
        {
          isLoadingChangedPaths = true;
          loadChangedPathsItem = item;
          ThreadPool.QueueUserWorkItem(LoadChangedPaths);
        }
      }
      else
      {
        int pathWidth = 70;
        int copyFromWidth = 70;
        using (Graphics g = CreateGraphics())
        {
          foreach (DictionaryEntry entry in changes)
          {
            string path = (string)entry.Key;
            path = path.Replace('\\', '/');
            SizeF size = g.MeasureString(path, changesList.Font);
            if (size.Width + 4 > pathWidth)
              pathWidth = (int)size.Width + 4;
            ChangedPath change = (ChangedPath)entry.Value;
            string copyFrom = change.CopyFromPath;
            if (copyFrom == null)
            {
              copyFrom = string.Empty;
            }
            else
            {
              copyFrom = copyFrom + " : r" + change.CopyFromRevision;
              size = g.MeasureString(copyFrom, changesList.Font);
              if (size.Width + 4 > copyFromWidth)
                copyFromWidth = (int)size.Width + 4;
            }
            ListViewItem newItem = new ListViewItem(new string[] {
						                                        	SvnClient.GetActionString(change.Action),
						                                        	path,
						                                        	copyFrom
						                                        });
            changesList.Items.Add(newItem);
          }
        }
        changesList.Columns[1].Width = pathWidth;
        changesList.Columns[2].Width = copyFromWidth;
      }
    }

    ListViewItem loadChangedPathsItem;
    volatile bool isLoadingChangedPaths;

    void LoadChangedPaths( object state )
    {
      try
      {
        LogMessage logMessage = (LogMessage)loadChangedPathsItem.Tag;
        string fileName = Path.GetFullPath(_viewContent.FileName);
        Client client = SvnClient.Instance.Client;
        try
        {
          client.Log(new string[] { fileName },
                     Revision.FromNumber(logMessage.Revision), // Revision start
                     Revision.FromNumber(logMessage.Revision), // Revision end
                     true,                   // bool discoverChangePath
                     false,                  // bool strictNodeHistory
                     new LogMessageReceiver(ReceiveChangedPaths));
        }
        catch (SvnClientException ex)
        {
          if (ex.ErrorCode == 160013)
          {
            // This can happen when the file was renamed/moved so it cannot be found
            // directly in the old revision. In that case, we do a full download of
            // all revisions (so the file can be found in the new revision and svn can
            // follow back its history).
            client.Log(new string[] { fileName },
                       Revision.FromNumber(1),            // Revision start
                       Revision.FromNumber(lastRevision), // Revision end
                       true,                   // bool discoverChangePath
                       false,                  // bool strictNodeHistory
                       new LogMessageReceiver(ReceiveAllChangedPaths));
          }
          else
          {
            throw;
          }
        }
        loadChangedPathsItem = null;
        isLoadingChangedPaths = false;
        HostServicesSingleton.SafeThreadAsyncCall<object, EventArgs>(this.RevisionListViewSelectionChanged, null, EventArgs.Empty);
      }
      catch (Exception ex)
      {
        MessageService.ShowError(ex);
      }
    }

    void ReceiveChangedPaths( LogMessage logMessage )
    {
      loadChangedPathsItem.Tag = logMessage;
    }

    void ReceiveAllChangedPaths( LogMessage logMessage )
    {
      //WorkbenchSingleton.SafeThreadAsyncCall(this.ReceiveAllChangedPathsInvoked, logMessage);
    }

    void ReceiveAllChangedPathsInvoked( LogMessage logMessage )
    {
      foreach (ListViewItem item in revisionList.Items)
      {
        LogMessage oldMessage = (LogMessage)item.Tag;
        if (oldMessage.Revision == logMessage.Revision)
        {
          item.Tag = logMessage;
          break;
        }
      }
    }
  }
}
