using CodeStatistics.Handling;
using CodeStatistics.Output;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CodeStatistics{
    static class Program{
        [STAThread]
        static void Main(string[] args){
            ProgramArguments arguments = new ProgramArguments(args);

            MainForm form = new MainForm();
            
            if (form.ShowDialog() == DialogResult.OK){
                Debug.WriteLine(form.SelectedFiles);
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
