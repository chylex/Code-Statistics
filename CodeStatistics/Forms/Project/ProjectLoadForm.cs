using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using CodeStatistics.Data;
using CodeStatistics.Forms.Utils;
using CodeStatistics.Input;
using CodeStatistics.Output;
using CodeStatisticsCore.Handling;

namespace CodeStatistics.Forms.Project{
    sealed partial class ProjectLoadForm : Form{
        private readonly IInputMethod inputMethod;
        private FileSearch search;
        private Handling.Project project;
        private Variables variables;

        private string outputFile;
        [Localizable(true)] private string lastOutputGenError;

        private ProjectLoadForm(){
            InitializeComponent();
            
            Text = Lang.Get["TitleProject"];
            btnCancel.Text = Lang.Get["LoadProjectCancel"];
            btnClose.Text = Lang.Get["LoadProjectClose"];
            btnOpenOutput.Text = Lang.Get["LoadProjectOpenOutput"];
            btnDebugProject.Text = Lang.Get["LoadProjectDebug"];
            btnBreakPoint.Text = Lang.Get["LoadProjectBreakpoint"];

            if (Program.Config.NoGui){
                Opacity = 0;
            }
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

            search.Refresh += count => this.InvokeSafe(() => {
                labelLoadData.Text = count.ToString(CultureInfo.InvariantCulture);
            });

            search.Finish += data => this.InvokeSafe(() => {
                labelLoadInfo.Text = Lang.Get["LoadProjectProcess"];
                labelLoadData.Text = "";
                UpdateProgress(ProgressBarStyle.Continuous, 0);

                project = new Handling.Project(data);

                project.Progress += (percentage, processedEntries, totalEntries) => this.InvokeSafe(() => {
                    UpdateProgress(ProgressBarStyle.Continuous, percentage);
                    labelLoadData.Text = Lang.Get["LoadProjectProcessingFiles", processedEntries, totalEntries];
                });

                project.Finish += vars => this.InvokeSafe(() => {
                    variables = Program.Config.IsDebuggingTemplate ? new Variables.Dummy() : vars;

                    labelLoadInfo.Text = Lang.Get["LoadProjectProcessingDone"];

                    btnCancel.Visible = false;
                    flowLayoutButtonsReady.Visible = true;

                    #if DEBUG
                    flowLayoutButtonsDebug.Visible = true;
                    #else
                    flowLayoutButtonsDebug.Visible = Program.Config.IsDebuggingProject;
                    #endif

                    while(!GenerateOutputFile()){
                        DialogResult res = MessageBox.Show(lastOutputGenError, Lang.Get["LoadProjectError"], MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);

                        if (res == DialogResult.Cancel){
                            DialogResult = DialogResult.Abort;
                            Close();
                            break;
                        }
                    }

                    if (Program.Config.AutoOpenBrowser){
                        btnOpenOutput_Click(null, new EventArgs());
                    }

                    if (Program.Config.CloseOnFinish){
                        DialogResult = DialogResult.Abort;
                        Close();
                    }

                    if (Program.Config.IsDebuggingTemplate){
                        string templateFile = Program.Config.GetCustomTemplateFilePath();
                        if (templateFile == null)return; // WTF

                        FileSystemWatcher watcher = new FileSystemWatcher{
                            Path = Path.GetDirectoryName(templateFile),
                            Filter = Path.GetFileName(templateFile),
                            NotifyFilter = NotifyFilters.LastWrite
                        };

                        watcher.Changed += (sender, args) => {
                            labelLoadData.Text = Lang.Get["LoadProjectDummyTemplateRebuild"];
                            labelLoadData.Text = GenerateOutputFile() ? Lang.Get["LoadProjectDummyTemplateWait"] : Lang.Get["LoadProjectDummyTemplateFailed"];
                        };

                        watcher.EnableRaisingEvents = true;

                        labelLoadData.Text = Lang.Get["LoadProjectDummyTemplateWait"];
                    }
                });

                project.Failure += ex => this.InvokeSafe(() => {
                    MessageBox.Show(Lang.Get["LoadProjectErrorProcessing", ex.ToString()], Lang.Get["LoadProjectError"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                });

                project.ProcessAsync();
            });

            search.Failure += ex => this.InvokeSafe(() => {
                MessageBox.Show(Lang.Get["LoadProjectErrorFileSearch", ex.ToString()], Lang.Get["LoadProjectError"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            });

            search.StartAsync();
        }

        private bool GenerateOutputFile(){
            string templateFile = Program.Config.GetTemplateFilePath();

            if (templateFile == null){
                lastOutputGenError = Lang.Get["LoadProjectErrorNoTemplate"];
                return false;
            }

            outputFile = Program.Config.GetOutputFilePath();

            GenerateHtml generator = new GenerateHtml(new TemplateReader(templateFile), variables); // TODO error handling

            switch(generator.ToFile(outputFile)){
                case GenerateHtml.Result.Succeeded:
                    return true;

                case GenerateHtml.Result.TemplateError:
                    lastOutputGenError = Lang.Get["LoadProjectErrorInvalidTemplate", generator.LastError];
                    return false;

                case GenerateHtml.Result.IoError:
                    lastOutputGenError = Lang.Get["LoadProjectErrorIO", generator.LastError];
                    return false;

                default:
                    lastOutputGenError = Lang.Get["LoadProjectErrorUnknown"];
                    return false;
            }
        }

        private void UpdateProgress(ProgressBarStyle style, int percentage){
            if (progressBarLoad.Style == ProgressBarStyle.Marquee && style == ProgressBarStyle.Marquee){
                return; // fix progress bar freaking out when re-setting marquee
            }

            progressBarLoad.Style = style;
            progressBarLoad.SetValueInstant(percentage*10);
        }

        private void btnCancel_Click(object sender, EventArgs e){
            if (variables == null){
                if (project != null)project.Cancel(() => this.InvokeSafe(OnCancel));
                else if (search != null)search.CancelProcess(() => this.InvokeSafe(OnCancel));
                else if (inputMethod != null)inputMethod.CancelProcess(() => this.InvokeSafe(OnCancel));
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

        private void btnOpenOutput_Click(object sender, EventArgs e){
            if (outputFile == null){
                return;
            }

            Process.Start(outputFile);
        }

        private void btnDebugProject_Click(object sender, EventArgs e){
            if (project == null || variables == null){
                return;
            }

            new ProjectDebugForm(project).ShowDialog();
        }

        private void btnBreakPoint_Click(object sender, EventArgs e){
            if (project == null || variables == null){
                return;
            }

            Debugger.Break();
        }

        public class UpdateCallbacks{
            private readonly ProjectLoadForm form;

            public UpdateCallbacks(ProjectLoadForm form){
                this.form = form;
            }

            public void UpdateInfoLabel([Localizable(true)] string text){
                form.InvokeSafe(() => form.labelLoadInfo.Text = text);
            }

            public void UpdateDataLabel([Localizable(true)] string text){
                form.InvokeSafe(() => form.labelLoadData.Text = text);
            }

            public void UpdateProgress(int progress){
                form.InvokeSafe(() => {
                    if (progress == -1){
                        form.UpdateProgress(ProgressBarStyle.Marquee, 100);
                    }
                    else{
                        form.UpdateProgress(ProgressBarStyle.Continuous, progress);
                    }
                });
            }

            public void OnReady(FileSearch fileSearch){
                form.InvokeSafe(() => form.OnReady(fileSearch));
            }
        }
    }
}
