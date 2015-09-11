﻿using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace CodeStatistics.Input.Methods{
    class MultiFolderDialog : IProjectInputMethod{
        public string[] Run(string[] args){
            if (Environment.OSVersion.Version.Major >= 6){ // vista and newer
                try{
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "Folders|\n";
                    dialog.CheckFileExists = false;
                    dialog.CheckPathExists = true;
                    dialog.DereferenceLinks = true;
                    dialog.AddExtension = false;
                    dialog.Multiselect = true;
                    dialog.AutoUpgradeEnabled = true;

                    Assembly assembly = LoadAssembly("System.Windows.Forms");
                    Type ifdType = assembly.GetType("System.Windows.Forms.FileDialogNative").GetNestedType("IFileDialog",BindingFlags.NonPublic);
                    Type fdType = typeof(FileDialog);
                    Type dialogType = dialog.GetType();
                    BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

                    var vistaDialog = dialogType.GetMethod("CreateVistaDialog",flags).Invoke(dialog,new object[0]);
                    dialogType.GetMethod("OnBeforeVistaDialog",flags).Invoke(dialog,new object[]{ vistaDialog });

                    uint opts = (uint)fdType.GetMethod("GetOptions",flags).Invoke(dialog,new object[0]);
                    ifdType.GetMethod("SetOptions",flags).Invoke(vistaDialog,new object[]{ opts | 32u }); // 32u = FOS_PICKFOLDERS

                    object events = fdType.GetNestedType("VistaDialogEvents",flags).GetConstructors()[0].Invoke(new object[]{ dialog });
                    object[] parameters = new object[]{ events, (uint)0 };
                    ifdType.GetMethod("Advise").Invoke(vistaDialog,parameters);
                    uint id = (uint)parameters[1];

                    try{
                        ifdType.GetMethod("Show").Invoke(vistaDialog,new object[]{ IntPtr.Zero });
                    }finally{
                        ifdType.GetMethod("Unadvise").Invoke(vistaDialog,new object[]{ id });
                        GC.KeepAlive(events);
                    }

                    return dialog.FileNames;
                }catch(Exception e){
                    Debug.Write(e.ToString());
                }
            }

            FolderBrowserDialog oldDialog = new FolderBrowserDialog();
            oldDialog.ShowNewFolderButton = false;
            oldDialog.ShowDialog();
            return new[]{ oldDialog.SelectedPath };
        }

        private static Assembly LoadAssembly(string name){
            foreach(AssemblyName ass in Assembly.GetExecutingAssembly().GetReferencedAssemblies()){
                if (ass.Name.Equals(name))return Assembly.Load(ass);
            }

            return null;
        }
    }
}
