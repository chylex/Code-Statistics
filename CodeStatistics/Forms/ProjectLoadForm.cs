using CodeStatistics.Handling;
using CodeStatistics.Input;
using System;
using System.Windows.Forms;
using CodeStatistics.Output;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using CodeStatistics.Data;
using CodeStatistics.Input.Methods;

namespace CodeStatistics.Forms{
    public partial class ProjectLoadForm : Form{
        private readonly IInputMethod inputMethod;
        private FileSearch search;
        private Project project;
        private Variables variables;

        private ProjectLoadForm(){
            InitializeComponent();
            
            this.Text = Lang.Get["TitleProject"];
            btnCancel.Text = Lang.Get["LoadProjectCancel"];
            btnClose.Text = Lang.Get["LoadProjectClose"];
            btnGenerateOutput.Text = Lang.Get["LoadProjectGenerate"];
            btnDebugProject.Text = Lang.Get["LoadProjectDebug"];
            btnBreakPoint.Text = Lang.Get["LoadProjectBreakpoint"];
        }

        public ProjectLoadForm(IInputMethod inputMethod) : this(){
            this.inputMethod = inputMethod;
        }

        private void OnLoad(object sender, EventArgs e){
            inputMethod.BeginProcess(OnReady);
        }

        private void OnReady(FileSearch fileSearch){
            this.search = fileSearch;
            BeginProjectLoad();
        }

        private void BeginProjectLoad(){
            labelLoadInfo.Text = Lang.Get["LoadProjectSearchIO"];
            labelLoadData.Text = "";

            search.Refresh += count => InvokeOnUIThread(() => {
                labelLoadData.Text = count.ToString(CultureInfo.InvariantCulture);
            });

            search.Finish += data => InvokeOnUIThread(() => {
                labelLoadInfo.Text = Lang.Get["LoadProjectProcess"];
                labelLoadData.Text = "";
                progressBarLoad.Value = 0;
                progressBarLoad.Style = ProgressBarStyle.Continuous;

                project = new Project(data);

                project.Progress += (percentage, processedEntries, totalEntries) => InvokeOnUIThread(() => {
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
                });

                project.Finish += vars => InvokeOnUIThread(() => {
                    variables = vars;

                    labelLoadInfo.Text = Lang.Get["LoadProjectProcessingDone"];

                    btnCancel.Visible = false;
                    btnClose.Visible = true;
                    btnGenerateOutput.Visible = true;

                    #if DEBUG
                        btnDebugProject.Visible = true;
                        btnBreakPoint.Visible = true;
                    #endif
                });

                project.ProcessAsync();
            });

            search.StartAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e){
            if (variables == null){
                if (project != null)project.Cancel(() => InvokeOnUIThread(OnCancel));
                else if (search != null)search.Cancel(() => InvokeOnUIThread(OnCancel));
                else return;
            }

            btnCancel.Enabled = false;
        }

        private void OnCancel(){
            DialogResult = DialogResult.Cancel;
            Close();
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

        private void InvokeOnUIThread(Action func){
            if (InvokeRequired)Invoke(func);
            else func();
        }
    }
}
