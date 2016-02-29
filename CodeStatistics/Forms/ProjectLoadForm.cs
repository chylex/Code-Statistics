﻿using CodeStatistics.Handling;
using CodeStatistics.Input;
using System;
using System.Windows.Forms;
using CodeStatistics.Output;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using CodeStatistics.Data;
using CodeStatistics.Input.Methods;
using CodeStatistics.Properties;

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
            inputMethod.BeginProcess(new UpdateCallbacks(this));
        }

        private void OnReady(FileSearch fileSearch){
            this.search = fileSearch;
            BeginProjectLoad();
        }

        private void BeginProjectLoad(){
            labelLoadInfo.Text = Lang.Get["LoadProjectSearchIO"];
            labelLoadData.Text = "";

            search.Refresh += count => this.InvokeOnUIThread(() => {
                labelLoadData.Text = count.ToString(CultureInfo.InvariantCulture);
            });

            search.Finish += data => this.InvokeOnUIThread(() => {
                labelLoadInfo.Text = Lang.Get["LoadProjectProcess"];
                labelLoadData.Text = "";
                UpdateProgress(ProgressBarStyle.Continuous,0);

                project = new Project(data);

                project.Progress += (percentage, processedEntries, totalEntries) => this.InvokeOnUIThread(() => {
                    UpdateProgress(ProgressBarStyle.Continuous,percentage);
                    labelLoadData.Text = processedEntries+" / "+totalEntries;
                });

                project.Finish += vars => this.InvokeOnUIThread(() => {
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

        private void UpdateProgress(ProgressBarStyle style, int percentage){
            progressBarLoad.Style = style;

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
        }

        private void btnCancel_Click(object sender, EventArgs e){
            if (variables == null){
                if (project != null)project.Cancel(() => this.InvokeOnUIThread(OnCancel));
                else if (search != null)search.CancelProcess(() => this.InvokeOnUIThread(OnCancel));
                else if (inputMethod != null)inputMethod.CancelProcess(() => this.InvokeOnUIThread(OnCancel));
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

            string template = Resources.ResourceManager.GetString("template",CultureInfo.InvariantCulture);
            if (template == null)return; // TODO

            new GenerateHtml(template.Split('\n','\r'),variables).ToFile("output.html");
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

        public class UpdateCallbacks{
            private readonly ProjectLoadForm form;

            public UpdateCallbacks(ProjectLoadForm form){
                this.form = form;
            }

            public void UpdateInfoLabel(string text){
                form.InvokeOnUIThread(() => form.labelLoadInfo.Text = text);
            }

            public void UpdateDataLabel(string text){
                form.InvokeOnUIThread(() => form.labelLoadData.Text = text);
            }

            public void UpdateProgress(int progress){
                form.InvokeOnUIThread(() => {
                    if (progress == -1)form.UpdateProgress(ProgressBarStyle.Marquee,100);
                    else form.UpdateProgress(ProgressBarStyle.Continuous,progress);
                });
            }

            public void OnReady(FileSearch fileSearch){
                form.InvokeOnUIThread(() => form.OnReady(fileSearch));
            }
        }
    }
}
