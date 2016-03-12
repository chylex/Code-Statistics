using CodeStatistics.Data;
using CodeStatistics.Input.Methods;
using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace CodeStatistics.Forms{
    public sealed partial class GitHubForm : Form{
        public GitHub GitHub { get; private set; }

        private readonly Timer timer = new Timer{
            Interval = 350,
            Enabled = false,
        };

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
            listBranches.SelectedText = GitHub.DefaultBranch;
        }

        private void btnDownload_Click(object sender, EventArgs e){
            GitHub = new GitHub(textBoxRepository.Text);
            GitHub.Branch = (string)listBranches.SelectedItem;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void textBoxRepository_TextChanged(object sender, EventArgs e){
            btnDownload.Enabled = false;
            timer.Stop();
            timer.Start();
        }

        private void listBranches_SelectedValueChanged(object sender, EventArgs e){
            if (listBranches.SelectedItem is ItemBranchTechnical){
                listBranches.SelectedIndex = -1;
            }
        }

        private void timer_Tick(object sender, EventArgs e){
            timer.Stop();

            if (GitHub.IsRepositoryValid(textBoxRepository.Text)){
                listBranches.Items.Add(new ItemBranchTechnical("LoadGitHubBranchLoading"));

                GitHub github = new GitHub(textBoxRepository.Text);

                GitHub.DownloadStatus status = github.RetrieveBranchList((branches, ex) => this.InvokeOnUIThread(() => {
                    github.Dispose();
                    listBranches.Items.Clear();

                    if (ex != null){
                        btnDownload.Enabled = false;
                        listBranches.Items.Add(new ItemBranchTechnical("LoadGitHubBranchFailure"));
#if MONO
                        Exception testEx = ex.InnerException;

                        while(testEx != null){
                            if (testEx.GetType().FullName == "Mono.Security.Protocol.Tls.TlsException"){
                                if (MessageBox.Show(Lang.Get["LoadGitHubTrustError"],Lang.Get["LoadGitHubError"],MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes){
                                    Process process = Process.Start("mozroots","--import --ask-remove --quiet");
                                    if (process != null)process.WaitForExit();

                                    timer_Tick(timer,new EventArgs());
                                }

                                break;
                            }

                            testEx = testEx.InnerException;
                        }
#endif
                        return;
                    }

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

                switch(status){
                    case GitHub.DownloadStatus.NoInternet:
                        MessageBox.Show(Lang.Get["LoadGitHubNoInternet"],Lang.Get["LoadGitHubError"],MessageBoxButtons.OK,MessageBoxIcon.Error);
                        break;

                    case GitHub.DownloadStatus.NoConnection:
                        MessageBox.Show(Lang.Get["LoadGitHubNoEstablishedConnection"],Lang.Get["LoadGitHubError"],MessageBoxButtons.OK,MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e){
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private class ItemBranchTechnical{
            private readonly string str;

            public ItemBranchTechnical(string langKey){
                this.str = Lang.Get[langKey];
            }

            public override string ToString(){
                return str;
            }
        }
    }
}
