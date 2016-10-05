using System;
using System.Windows.Forms;
using CodeStatistics.Forms;
using CodeStatistics.Data;
using CodeStatistics.Forms.Project;
using CodeStatistics.Input;
using CodeStatisticsCore.Input;

namespace CodeStatistics{
    static class Program{
        public const string Version = "0.9-Beta";
        public const string VersionFull = "0.9.0.0";

        public static ProgramConfiguration Config { get; private set; }

        [STAThread]
        private static void Main(string[] args){
            Application.EnableVisualStyles();

            #if WINDOWS
            if (IsRunningMono()){
                MessageBox.Show(Lang.Get["ErrorLaunchMonoOnWindowsBuild"], Lang.Get["ErrorLaunchTitle"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endif

            ProgramArguments programArgs = new ProgramArguments(args, ProgramConfiguration.Validate);

            if (programArgs.HasError){
                MessageBox.Show(programArgs.Error, Lang.Get["ErrorInvalidArgsTitle"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Config = new ProgramConfiguration(programArgs);
            Start();

            IOUtils.CleanupTemporaryDirectory();
        }

        private static void Start(){
            IInputMethod immediateInput = Config.GetImmediateInputMethod();

            if (immediateInput != null){
                new ProjectLoadForm(immediateInput).ShowDialog();
                return;
            }

            while(true){
                MainForm form = new MainForm();
            
                if (form.ShowDialog() == DialogResult.OK){
                    DialogResult result = new ProjectLoadForm(form.InputMethod).ShowDialog();
                    if (result == DialogResult.Abort)return;
                }
                else return;
            }
        }

        public static bool IsRunningMono(){
            return Type.GetType("Mono.Runtime") != null;
        }
    }
}
