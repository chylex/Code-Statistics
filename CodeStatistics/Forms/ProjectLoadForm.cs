using CodeStatistics.Handling;
using CodeStatistics.Input;
using System;
using System.Windows.Forms;
using CodeStatistics.Output;
using System.Diagnostics;
using System.IO;

namespace CodeStatistics.Forms{
    public partial class ProjectLoadForm : Form{
        private readonly FileSearch search;
        private Project project;
        private Variables variables;

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
                            int percValue = percentage*10;

                            // instant progress bar update hack
                            if (percValue == progressBarLoad.Maximum){
                                progressBarLoad.Value = percValue;
                                progressBarLoad.Value = percValue-1;
                                progressBarLoad.Value = percValue;
                            }
                            else{
                                progressBarLoad.Value = percValue+1;
                                progressBarLoad.Value = percValue;
                            }

                            labelLoadData.Text = processedEntries+" / "+totalEntries;
                        }));
                    };

                    project.Finish += vars => {
                        Invoke(new MethodInvoker(() => {
                            variables = vars;

                            labelLoadInfo.Text = "Project processing finished.";

                            btnCancel.Visible = false;
                            btnClose.Visible = true;
                            btnGenerateOutput.Visible = true;

                            #if DEBUG
                                btnDebugProject.Visible = true;
                                btnBreakPoint.Visible = true;
                            #endif
                        }));
                    };

                    project.ProcessAsync();
                }));
            };

            search.StartAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e){
            if (variables != null);
            else if (project != null)project.Cancel(OnCancel);
            else if (search != null)search.Cancel(OnCancel);
            else return;

            btnCancel.Enabled = false;
        }

        private void OnCancel(){
            Invoke(new MethodInvoker(() => {
                DialogResult = DialogResult.Cancel;
                Close();
            }));
        }

        private void btnClose_Click(object sender, EventArgs e){
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnGenerateOutput_Click(object sender, EventArgs e){
            if (project == null || variables == null)return;

            new GenerateHtml(System.IO.File.ReadAllLines("template.html"),variables).ToFile("output.html");
            Process.Start(Path.Combine(Directory.GetCurrentDirectory(),"output.html"));
        }

        private void btnDebugProject_Click(object sender, EventArgs e){
            if (project == null || variables == null)return;

            new ProjectDebugForm(project).ShowDialog();
        }

        private void btnBreakPoint_Click(object sender, EventArgs e){
            if (project == null || variables == null)return;

            Debugger.Break();
        }
    }
}
