using CodeStatistics.Handling;
using CodeStatistics.Input;
using System;
using System.Windows.Forms;

namespace CodeStatistics.Forms{
    public partial class ProjectLoadForm : Form{
        private readonly FileSearch search;
        private Project project;

        public ProjectLoadForm(string[] rootFiles){
            InitializeComponent();
            search = new FileSearch(rootFiles);
        }

        private void OnLoad(object sender, EventArgs e){
            labelLoadInfo.Text = "Searching Files and Folders...";
            labelLoadData.Text = "";

            search.Refresh += count => {
                Invoke(new MethodInvoker(() => {
                    labelLoadData.Text = count.ToString();
                }));
            };

            search.Finish += data => {
                Invoke(new MethodInvoker(() => {
                    labelLoadInfo.Text = "Processing the Project...";
                    labelLoadData.Text = "";
                    progressBarLoad.Value = 0;
                    progressBarLoad.Style = ProgressBarStyle.Continuous;

                    project = new Project(data);

                    project.Progress += (percentage, processedEntries, totalEntries) => {
                        Invoke(new MethodInvoker(() => {
                            progressBarLoad.Value = Math.Min(100,percentage+1);
                            progressBarLoad.Value = percentage; // instant progress bar update hack

                            labelLoadData.Text = processedEntries+" / "+totalEntries;
                        }));
                    };

                    project.ProcessAsync();
                }));
            };

            search.StartAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e){
            if (project != null)project.Cancel();
            else if (search != null)search.Cancel();
            else return;

            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
