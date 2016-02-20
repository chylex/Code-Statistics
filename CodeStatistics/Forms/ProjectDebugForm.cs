using CodeStatistics.Handling;
using System.Windows.Forms;
using CodeStatistics.Input;
using System;
using System.Runtime.InteropServices;
using CodeStatistics.Handling.Languages;
using PathIO = System.IO.Path;

namespace CodeStatistics.Forms{
    partial class ProjectDebugForm : Form{
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr handle, int message, IntPtr wParam, int[] lParam);

        public ProjectDebugForm(Project project){
            InitializeComponent();

            foreach(File file in project.SearchData.Files){
                if (HandlerList.GetFileHandler(file) is AbstractLanguageFileHandler){
                    listBoxFiles.Items.Add(new RelativeFile(project.SearchData.Root,file));
                }
            }

            listBoxFiles_SelectedValueChange(listBoxFiles,new EventArgs());

            SendMessage(textBoxCode.Handle,0x00CB,new IntPtr(1),new []{ 16 });
        }

        private void listBoxFiles_SelectedValueChange(object sender, EventArgs e){
            RelativeFile item = listBoxFiles.SelectedItem as RelativeFile;
            if (item == null)return;

            textBoxCode.Text = ((AbstractLanguageFileHandler)HandlerList.GetFileHandler(item.File)).PrepareFileContents(item.File);
        }

        private class RelativeFile{
            private readonly string root;
            public readonly File File;

            public RelativeFile(string root, File file){
                this.root = root;
                this.File = file;
            }

            public override string ToString(){
                return File.FullPath.Substring(root.Length+1);
            }
        }
    }
}
