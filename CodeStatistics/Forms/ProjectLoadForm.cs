using CodeStatistics.Input;
using System;
using System.Windows.Forms;

namespace CodeStatistics.Forms{
    public partial class ProjectLoadForm : Form{
        private readonly FileSearch search;
        private FileSearchData searchData;

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
                    searchData = data;

                    labelLoadInfo.Text = "Processing the Project...";
                    labelLoadData.Text = "";
                    progressBarLoad.Value = 0;
                    progressBarLoad.Style = ProgressBarStyle.Continuous;

                    // TODO process
                }));
            };

            search.StartAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e){
            if (search == null || searchData != null)return;

            search.Cancel();

            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
