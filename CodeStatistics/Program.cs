using System;
using System.Windows.Forms;
using CodeStatistics.Forms;
using CodeStatistics.Input;
using CodeStatistics.Data;

[assembly:CLSCompliant(true)]
namespace CodeStatistics{
    static class Program{
        public static ProgramConfiguration Config { get; private set; }

        [STAThread]
        static void Main(string[] args){
            Application.EnableVisualStyles();

            AppDomain.CurrentDomain.FirstChanceException += (sender, ex) => {
                if (ex.Exception is EntryPointNotFoundException)return;
                System.Diagnostics.Debug.WriteLine("OOPS - Breakpoint");
            };

            ProgramArguments programArgs = new ProgramArguments(args,ProgramConfiguration.Validate);

            if (programArgs.HasError){
                MessageBox.Show(programArgs.Error,Lang.Get["ErrorInvalidArgsTitle"],MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            Config = new ProgramConfiguration(programArgs);

            while(true){
                MainForm form = new MainForm();
            
                if (form.ShowDialog() == DialogResult.OK){
                    ProjectLoadForm loadForm = new ProjectLoadForm(form.InputMethod);

                    DialogResult result = loadForm.ShowDialog();
                    if (result == DialogResult.Cancel)continue;
                    
                    // TODO
                }
                else break;
            }

            IOUtils.CleanupTemporaryDirectory();
        }
    }
}
