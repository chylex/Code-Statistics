using CodeStatistics.Input;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CodeStatistics{
    public partial class MainForm : Form{
        public MainForm(){
            InitializeComponent();
        }

        // Drag Events

        private void OnDragEnter(object sender, DragEventArgs e){
            e.Effect = e.Data.GetDataPresent("FileDrop") ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void OnDragDrop(object sender, DragEventArgs e){
            if (e.Data.GetDataPresent("FileDrop")){
                object fileDropData = e.Data.GetData("FileDrop");
                string[] files = fileDropData as string[];

                if (files == null){
                    string file = fileDropData as string;
                    if (file != null)files = new[]{ file };
                }

                if (files == null || files.Length == 0)return;

                // TODO
            }
        }

        // Button Click Events

        private void btnProjectFolder_Click(object sender, EventArgs e){
            string[] folders = MultiFolderDialog.Show(this);
            
            if (folders.Length != 0){
                // TODO
            }
        }

        private void btnViewSourceCode_Click(object sender, EventArgs e){
            Process.Start("https://github.com/chylex/Code-Statistics");
        }
    }
}
