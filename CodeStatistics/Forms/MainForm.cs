using System;
using System.Diagnostics;
using System.Windows.Forms;
using CodeStatistics.Data;
using CodeStatistics.Input.Methods;
using CodeStatistics.Input;
using CodeStatistics.Input.Helpers;
using System.Drawing;
using CodeStatisticsCore.Input;

namespace CodeStatistics.Forms{
    sealed partial class MainForm : Form{
        public IInputMethod InputMethod { get; private set; }

        public MainForm(){
            InitializeComponent();
            
            Text = Lang.Get["Title"];
            btnProjectFolder.Text = Lang.Get["MenuProjectFromFolder"];
            btnProjectArchive.Text = Lang.Get["MenuProjectFromArchive"];
            btnProjectGitHub.Text = Lang.Get["MenuProjectFromGitHub"];
            btnViewSourceCode.Text = Lang.Get["MenuViewSourceCode"];
            btnViewAbout.Text = Lang.Get["MenuViewAbout"];

            if (!ZipArchive.CheckZipSupport()){
                btnProjectArchive.Enabled = false;
                btnProjectArchive.BackColor = Color.WhiteSmoke;
            }
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

                if (files.Length == 1 && ZipArchive.CanHandleFile(files[0])){
                    InputMethod = new ArchiveExtraction(files[0],IOUtils.CreateTemporaryDirectory());
                }
                else{
                    InputMethod = new FileSearch(files);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        // Button Click Events

        private void btnProjectFolder_Click(object sender, EventArgs e){
            string[] folders = MultiFolderDialog.Show(this);
            
            if (folders.Length != 0){
                InputMethod = new FileSearch(folders);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnProjectArchive_Click(object sender, EventArgs e){
            OpenFileDialog dialog = new OpenFileDialog{
                Filter = Lang.Get["DialogFilterArchives"]+ArchiveExtraction.FilterArchives,
                CheckFileExists = true,
                DereferenceLinks = true,
                AutoUpgradeEnabled = true
            };

            if (dialog.ShowDialog() == DialogResult.OK){
                InputMethod = new ArchiveExtraction(dialog.FileName,IOUtils.CreateTemporaryDirectory());
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnProjectGitHub_Click(object sender, EventArgs e){
            GitHubForm form = new GitHubForm();
            
            if (form.ShowDialog() == DialogResult.OK){
                InputMethod = form.GitHub;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnViewSourceCode_Click(object sender, EventArgs e){
            Process.Start("https://github.com/chylex/Code-Statistics");
        }

        private void btnViewAbout_Click(object sender, EventArgs e){
            new AboutForm().ShowDialog();
        }
    }
}
