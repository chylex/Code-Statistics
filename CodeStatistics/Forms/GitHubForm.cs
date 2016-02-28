using CodeStatistics.Input.Methods;
using System;
using System.Windows.Forms;

namespace CodeStatistics.Forms{
    public partial class GitHubForm : Form{
        public GitHub GitHub { get; private set; }

        public GitHubForm(){
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e){
            listBranches.Items.Add("master");
            listBranches.SelectedIndex = 0;
        }

        private void btnDownload_Click(object sender, EventArgs e){
            string[] data = textBoxRepository.Text.Split('/');
            GitHub = new GitHub(data[0],data[1]);
            GitHub.Branch = (string)listBranches.SelectedItem;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnListBranches_Click(object sender, EventArgs e){
            string[] data = textBoxRepository.Text.Split('/');
            GitHub github = new GitHub(data[0],data[1]);

            github.RetrieveBranchList(branches => this.InvokeOnUIThread(() => {
                listBranches.Items.Clear();

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
