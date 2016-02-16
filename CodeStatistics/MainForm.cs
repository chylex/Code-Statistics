using CodeStatistics.Input;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CodeStatistics{
    public partial class MainForm : Form{
        public MainForm(){
            InitializeComponent();
        }


        private void btnProjectFolder_Click(object sender, EventArgs e){
            string[] folders = MultiFolderDialog.Show(this);
            
            if (folders.Length != 0){
                // TODO
            }
        }
        private void btnViewSourceCode_Click(object sender, EventArgs e){
            Process.Start("https://github.com/chylex/Code-Statistics");
        }
    }
}
