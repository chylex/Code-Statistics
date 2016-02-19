using System;
using System.Windows.Forms;
using CodeStatistics.Forms;

namespace CodeStatistics{
    static class Program{
        [STAThread]
        static void Main(string[] args){
            Application.EnableVisualStyles();

            AppDomain.CurrentDomain.FirstChanceException += (sender, ex) => {
                if (ex.Exception is EntryPointNotFoundException)return;
                System.Diagnostics.Debug.WriteLine("OOPS - Breakpoint");
            };

            ProgramArguments arguments = new ProgramArguments(args);

            while(true){
                MainForm form = new MainForm();
            
                if (form.ShowDialog() == DialogResult.OK){
                    ProjectLoadForm loadForm = new ProjectLoadForm(form.SelectedFiles);

                    DialogResult result = loadForm.ShowDialog();
                    if (result == DialogResult.Cancel)continue;
                    
                    // TODO
                }
                else break;
            }
        }
    }
}
