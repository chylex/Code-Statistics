using CodeStatistics.Data;
using CodeStatistics.Input.Methods;
using System;
using System.Windows.Forms;

namespace CodeStatistics.Forms{
    public partial class GitHubForm : Form{
        public GitHub GitHub { get; private set; }

        private readonly Timer timer = new Timer{
            Interval = 350,
            Enabled = false,
        };

        private readonly ItemBranchLoading branchLoading = new ItemBranchLoading();
        private object previousSelectedBranch;

        public GitHubForm(){
            InitializeComponent();

            labelRepository.Text = Lang.Get["LoadGitHubRepositoryName"];
            labelBranch.Text = Lang.Get["LoadGitHubBranch"];
            btnDownload.Text = Lang.Get["LoadGitHubDownload"];
            btnCancel.Text = Lang.Get["LoadGitHubCancel"];

            if (Program.Config.NoGui)Opacity = 0;

            Disposed += (sender, args) => {
                if (GitHub != null)GitHub.Dispose();
                timer.Dispose();
            };

            timer.Tick += timer_Tick;
        }

        private void OnLoad(object sender, EventArgs e){
            ActiveControl = textBoxRepository;
            listBranches.Text = GitHub.DefaultBranch;
        }

        private void btnDownload_Click(object sender, EventArgs e){
            GitHub = new GitHub(textBoxRepository.Text);
            GitHub.Branch = (string)listBranches.SelectedItem;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void textBoxRepository_TextChanged(object sender, EventArgs e){
            timer.Stop();
            timer.Start();
        }

        private void listBranches_SelectedValueChanged(object sender, EventArgs e){
            if (listBranches.SelectedItem == branchLoading){
                listBranches.SelectedItem = previousSelectedBranch;
            }

            previousSelectedBranch = listBranches.SelectedItem;
        }

        private void timer_Tick(object sender, EventArgs e){
            timer.Stop();

            if (GitHub.IsRepositoryValid(textBoxRepository.Text)){
                listBranches.Items.Add(branchLoading);

                GitHub github = new GitHub(textBoxRepository.Text);

                github.RetrieveBranchList(branches => this.InvokeOnUIThread(() => {
                    github.Dispose();

                    listBranches.Items.Clear();

                    if (branches == null){
                        btnDownload.Enabled = false;
                        return;
                    }

                    foreach(string branch in branches){
                        listBranches.Items.Add(branch);

                        if (branch == listBranches.Text){
                            listBranches.SelectedIndex = listBranches.Items.Count-1;
                        }
                    }

                    btnDownload.Enabled = true;
                }));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e){
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private class ItemBranchLoading{
            public override string ToString(){
                return Lang.Get["LoadGitHubBranchLoading"];
            }
        }
    }
}
