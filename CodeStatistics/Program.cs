using CodeStatistics.Handling;
using CodeStatistics.Output;
using System;
using System.Windows.Forms;
using CodeStatistics.Forms;

namespace CodeStatistics{
    static class Program{
        [STAThread]
        static void Main(string[] args){
            Application.EnableVisualStyles();

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

            if (true)return;


            /*FileSearch search = new FileSearch(rootFiles);

            search.Refresh += fileCount => {
                console.WriteCenter(centerY+1,fileCount.ToString());
            };

            HashSet<File> foundFiles = search.Search();
            
            ProjectAnalyzer analyzer = new ProjectAnalyzer(foundFiles);
            analyzer.Update += (percentage, handledFiles, totalFiles) => {};*/
        }
    }
}
