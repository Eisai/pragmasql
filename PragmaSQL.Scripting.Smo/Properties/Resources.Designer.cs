﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PragmaSQL.Scripting.Smo.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PragmaSQL.Scripting.Smo.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you want to copy data ?
        ///
        ///Source        : {0}
        ///Destination : {1}.
        /// </summary>
        internal static string CopyDataConfirmation {
            get {
                return ResourceManager.GetString("CopyDataConfirmation", resourceCulture);
            }
        }
        
        internal static System.Drawing.Bitmap database_connect {
            get {
                object obj = ResourceManager.GetObject("database_connect", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap database_lightning {
            get {
                object obj = ResourceManager.GetObject("database_lightning", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap database_save {
            get {
                object obj = ResourceManager.GetObject("database_save", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///	sc.name
        ///	, COLUMNPROPERTY( OBJECT_ID(&apos;{0}.{1}&apos;),sc.name,&apos;IsComputed&apos;)AS &apos;IsComputed&apos; 
        ///FROM  syscolumns sc 
        ///JOIN sysobjects so on sc.id = so.id
        ///JOIN sysusers su on so.uid = su.uid
        ///WHERE su.name = &apos;{2}&apos; AND so.name = &apos;{3}&apos;.
        /// </summary>
        internal static string Script_ColumnMap {
            get {
                return ResourceManager.GetString("Script_ColumnMap", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to -- disable referential integrity
        ///EXEC sp_MSForEachTable &apos;ALTER TABLE ? NOCHECK CONSTRAINT ALL&apos;
        ///-- Disable all triggers
        ///EXEC sp_MSForEachTable &apos;ALTER TABLE ? DISABLE TRIGGER ALL&apos;
        ///-- delete all data
        ///EXEC sp_MSForEachTable &apos;DELETE FROM ?&apos;
        ///-- enable referential integrity again
        ///EXEC sp_MSForEachTable &apos;ALTER TABLE ? CHECK CONSTRAINT ALL&apos;
        ///-- enable all trgiggers
        ///EXEC sp_MSForEachTable &apos;ALTER TABLE ? ENABLE TRIGGER ALL&apos;.
        /// </summary>
        internal static string Script_DeleteAllDatabaseData {
            get {
                return ResourceManager.GetString("Script_DeleteAllDatabaseData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT
        ///	id,
        ///	[name]
        ///	, CASE xtype 
        ///		WHEN &apos;U&apos; THEN 1
        ///		WHEN &apos;FN&apos; THEN 2
        ///		WHEN &apos;TF&apos; THEN 2
        ///		WHEN &apos;IF&apos; THEN 2
        ///		WHEN &apos;P&apos; THEN 3
        ///		WHEN &apos;X&apos; THEN 4
        ///		WHEN &apos;TR&apos; THEN 5
        ///	  ELSE 1000 END atype
        ///FROM sysobjects ORDER BY atype,[name]
        ///
        ///WHERE LOWER(NAME) IN ({0}).
        /// </summary>
        internal static string Script_ObjectMatch {
            get {
                return ResourceManager.GetString("Script_ObjectMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You have selected TableLock option, hovewer due to a bug in 
        ///SQL Server 2005 any errors during bulk copy may cause a server
        ///restart.
        ///
        ///If you applied Service Pack 2 to your SQL Server 2005 installation please
        ///discard this notfication.
        ///
        ///.
        /// </summary>
        internal static string TableLock_Notification {
            get {
                return ResourceManager.GetString("TableLock_Notification", resourceCulture);
            }
        }
    }
}
