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

        public GitHubForm(){
            InitializeComponent();

            if (Program.Config.NoGui)Opacity = 0;

            Disposed += (sender, args) => {
                if (GitHub != null)GitHub.Dispose();
                timer.Dispose();
            };

            timer.Tick += timer_Tick;
        }

        private void OnLoad(object sender, EventArgs e){
            ActiveControl = textBoxRepository;

            listBranches.Items.Add(GitHub.DefaultBranch);
            listBranches.SelectedIndex = 0;
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

        private void timer_Tick(object sender, EventArgs e){
            timer.Stop();

            if (GitHub.IsRepositoryValid(textBoxRepository.Text)){
                GitHub github = new GitHub(textBoxRepository.Text);

                github.RetrieveBranchList(branches => this.InvokeOnUIThread(() => {
                    github.Dispose();
                    if (branches == null)return;

                    listBranches.Items.Clear();

                    foreach(string branch in branches){
                        listBranches.Items.Add(branch);

                        if (branch == listBranches.Text){
                            listBranches.SelectedIndex = listBranches.Items.Count-1;
                        }
                    }
                }));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e){
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
