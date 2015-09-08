using System.Windows.Forms;

namespace CodeStatistics.Input{
    class MultiFolderDialog : IProjectInputMethod{
        public string[] Run(string[] args){
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Folders|\n";
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.DereferenceLinks = true;
            dialog.AddExtension = false;
            dialog.Multiselect = true;
            dialog.AutoUpgradeEnabled = true;
            dialog.ShowDialog();
            return dialog.FileNames;
        }
    }
}
