using CodeStatistics.Handling;
using System.Windows.Forms;
using System;
using System.Runtime.InteropServices;
using PathIO = System.IO.Path;
using CodeStatistics.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using CodeStatisticsCore.Handling;
using CodeStatisticsCore.Handling.Files;
using CodeStatisticsCore.Input;

namespace CodeStatistics.Forms{
    sealed partial class ProjectDebugForm : Form{
#if WINDOWS
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr handle, int message, IntPtr wParam, int[] lParam);
#endif

        private readonly List<RelativeFile> entries = new List<RelativeFile>(64);

        public ProjectDebugForm(Project project){
            InitializeComponent();

            Text = Lang.Get["TitleDebug"];
            btnReprocess.Text = Lang.Get["DebugProjectReprocess"];
            btnLoadOriginal.Text = Lang.Get["DebugProjectLoadOriginal"];
            btnDebug.Text = Lang.Get["DebugProjectDebug"];

            foreach(File file in project.SearchData.Files.Where(file => HandlerList.GetFileHandler(file) is AbstractLanguageFileHandler)){
                entries.Add(new RelativeFile(project.SearchData.Root, file));
            }

            textBoxFilterFiles_TextChanged(textBoxFilterFiles, new EventArgs());
            listBoxFiles_SelectedValueChange(listBoxFiles, new EventArgs());

#if WINDOWS
            SendMessage(textBoxCode.Handle, 0x00CB, new IntPtr(1), new []{ 16 });
#endif
        }

        private void textBoxFilterFiles_TextChanged(object sender, EventArgs e){
            listBoxFiles.Items.Clear();
            listBoxFiles.BeginUpdate();

            if (textBoxFilterFiles.Text.Length == 0){
                foreach(RelativeFile file in entries){
                    listBoxFiles.Items.Add(file);
                }
            }
            else{
                foreach(RelativeFile file in entries){
                    if (file.RelativePath.Contains(textBoxFilterFiles.Text)){
                        listBoxFiles.Items.Add(file);
                    }
                }
            }

            listBoxFiles.EndUpdate();
        }

        private void listBoxFiles_SelectedValueChange(object sender, EventArgs e){
            RelativeFile item = listBoxFiles.SelectedItem as RelativeFile;
            if (item == null)return;
            
            AbstractLanguageFileHandler handler = GetLanguageHandler(item.File);

            SetTextBoxContents(handler.PrepareFileContents(item.File.Contents));

            treeViewData.Nodes.Clear();
            foreach(TreeNode node in handler.GenerateTreeViewData(GenerateVariables(item.File), item.File))treeViewData.Nodes.Add(node);
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

            Variables.Root variables = GenerateVariables(item.File);
            variables.CheckFlag(""); // keep the object alive for debugging

            Debugger.Break();
        }

        private void SetTextBoxContents(string text){
            textBoxCode.Text = text.Replace("\r", "").Replace("\n", Environment.NewLine);
        }

        private static AbstractLanguageFileHandler GetLanguageHandler(File file){
            return (AbstractLanguageFileHandler)HandlerList.GetFileHandler(file);
        }

        private static Variables.Root GenerateVariables(File file){
            AbstractLanguageFileHandler handler = GetLanguageHandler(file);
            Variables.Root variables = new Variables.Root();

            handler.SetupProject(variables);
            handler.Process(file, variables);
            handler.FinalizeProject(variables);

            return variables;
        }

        private class RelativeFile{
            public readonly File File;
            public readonly string RelativePath;

            public RelativeFile(string root, File file){
                this.File = file;
                this.RelativePath = File.FullPath.Substring(root.Length+1);
            }

            public override string ToString(){
                return RelativePath;
            }
        }
    }
}
