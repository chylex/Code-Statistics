using CodeStatistics.Input.Methods;
using System;
using System.Windows.Forms;

namespace CodeStatistics.Forms{
    public partial class GitHubForm : Form{
        public GitHub GitHub { get; private set; }

        public GitHubForm(){
            InitializeComponent();

            if (Program.Config.NoGui)Opacity = 0;

            Disposed += (sender, args) => {
                if (GitHub != null)GitHub.Dispose();
            };
        }

        private void OnLoad(object sender, EventArgs e){
            listBranches.Items.Add(GitHub.DefaultBranch);
            listBranches.SelectedIndex = 0;
        }

        private void btnDownload_Click(object sender, EventArgs e){
            GitHub = new GitHub(textBoxRepository.Text);
            GitHub.Branch = (string)listBranches.SelectedItem;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnListBranches_Click(object sender, EventArgs e){
            GitHub github = new GitHub(textBoxRepository.Text);

            github.RetrieveBranchList(branches => this.InvokeOnUIThread(() => {
                listBranches.Items.Clear();
                if (branches == null)return;

                foreach(string branch in branches){
                    listBranches.Items.Add(branch);
                }
            }));
        }

        private void btnCancel_Click(object sender, EventArgs e){
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
