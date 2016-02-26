using CodeStatistics.Handling;
using System.Windows.Forms;
using CodeStatistics.Input;
using System;
using System.Runtime.InteropServices;
using CodeStatistics.Handling.Languages;
using PathIO = System.IO.Path;
using CodeStatistics.Data;
using System.Diagnostics;

namespace CodeStatistics.Forms{
    partial class ProjectDebugForm : Form{
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr handle, int message, IntPtr wParam, int[] lParam);

        public ProjectDebugForm(Project project){
            InitializeComponent();

            this.Text = Lang.Get["TitleDebug"];
            btnReprocess.Text = Lang.Get["DebugProjectReprocess"];
            btnLoadOriginal.Text = Lang.Get["DebugProjectLoadOriginal"];
            btnDebug.Text = Lang.Get["DebugProjectDebug"];

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
            
            SetTextBoxContents(GetLanguageHandler(item.File).PrepareFileContents(item.File.Contents));
        }

        private void btnLoadOriginal_Click(object sender, EventArgs e){
            RelativeFile item = listBoxFiles.SelectedItem as RelativeFile;
            if (item == null)return;

            SetTextBoxContents(item.File.Contents);
        }

        private void btnReprocess_Click(object sender, EventArgs e){
            RelativeFile item = listBoxFiles.SelectedItem as RelativeFile;
            if (item == null)return;

            SetTextBoxContents(GetLanguageHandler(item.File).PrepareFileContents(textBoxCode.Text));
        }

        private void btnDebug_Click(object sender, EventArgs e){
            RelativeFile item = listBoxFiles.SelectedItem as RelativeFile;
            if (item == null)return;

            AbstractLanguageFileHandler handler = GetLanguageHandler(item.File);
            Variables.Root variables = new Variables.Root();

            handler.SetupProject(variables);
            handler.Process(item.File,variables);
            handler.FinalizeProject(variables);

            Debugger.Break();
        }

        private void SetTextBoxContents(string text){
            textBoxCode.Text = text.Replace("\r","").Replace("\n",Environment.NewLine);
        }

        private static AbstractLanguageFileHandler GetLanguageHandler(File file){
            return (AbstractLanguageFileHandler)HandlerList.GetFileHandler(file);
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
