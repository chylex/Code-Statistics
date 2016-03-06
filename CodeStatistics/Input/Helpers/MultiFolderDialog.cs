using CodeStatistics.Data;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace CodeStatistics.Input.Helpers{
    static class MultiFolderDialog{
        private const string FilterFolders = "|\n";

        public static string[] Show(Form parent){
            if (Environment.OSVersion.Version.Major >= 6){ // vista and newer
                try{
                    OpenFileDialog dialog = new OpenFileDialog{
                        Filter = Lang.Get["DialogFilterFolders"]+FilterFolders,
                        CheckFileExists = false,
                        CheckPathExists = true,
                        DereferenceLinks = true,
                        AddExtension = false,
                        Multiselect = true,
                        AutoUpgradeEnabled = true
                    };

                    Assembly assembly = LoadAssembly("System.Windows.Forms");
                    Type ifdType = assembly.GetType("System.Windows.Forms.FileDialogNative").GetNestedType("IFileDialog",BindingFlags.NonPublic);
                    Type fdType = typeof(FileDialog);
                    Type dialogType = dialog.GetType();

                    const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

                    var vistaDialog = dialogType.GetMethod("CreateVistaDialog",flags).Invoke(dialog,new object[0]);
                    dialogType.GetMethod("OnBeforeVistaDialog",flags).Invoke(dialog,new object[]{ vistaDialog });

                    uint opts = (uint)fdType.GetMethod("GetOptions",flags).Invoke(dialog,new object[0]);
                    ifdType.GetMethod("SetOptions",flags).Invoke(vistaDialog,new object[]{ opts | 32u }); // 32u = FOS_PICKFOLDERS

                    object events = fdType.GetNestedType("VistaDialogEvents",flags).GetConstructors()[0].Invoke(new object[]{ dialog });
                    object[] parameters = { events, (uint)0 };
                    ifdType.GetMethod("Advise").Invoke(vistaDialog,parameters);
                    uint id = (uint)parameters[1];

                    try{
                        ifdType.GetMethod("Show").Invoke(vistaDialog,new object[]{ parent.Handle });
                    }finally{
                        ifdType.GetMethod("Unadvise").Invoke(vistaDialog,new object[]{ id });
                        GC.KeepAlive(events);
                    }

                    return dialog.FileNames;
                }catch(Exception e){
                    Debug.Write(e.ToString());
                }
            }

            var oldDialog = new FolderBrowserDialog{ ShowNewFolderButton = false };
            return oldDialog.ShowDialog(parent) == DialogResult.OK ? new string[]{ oldDialog.SelectedPath } : new string[0];
        }

        private static Assembly LoadAssembly(string name){
            foreach(AssemblyName ass in Assembly.GetExecutingAssembly().GetReferencedAssemblies()){
                if (ass.Name.Equals(name))return Assembly.Load(ass);
            }

            return null;
        }
    }
}
