using System;
using System.Windows.Forms;
using CodeStatistics.Forms;
using CodeStatistics.Input;
using CodeStatistics.Data;
using CodeStatistics.Input.Methods;

[assembly:CLSCompliant(true)]
namespace CodeStatistics{
    static class Program{
        public static ProgramConfiguration Config { get; private set; }

        [STAThread]
        static void Main(string[] args){
            Application.EnableVisualStyles();

            ProgramArguments programArgs = new ProgramArguments(args,ProgramConfiguration.Validate);

            if (programArgs.HasError){
                MessageBox.Show(programArgs.Error,Lang.Get["ErrorInvalidArgsTitle"],MessageBoxButtons.OK,MessageBoxIcon.Error);
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
