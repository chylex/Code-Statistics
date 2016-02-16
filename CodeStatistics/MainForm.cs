using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CodeStatistics{
    public partial class MainForm : Form{
        public MainForm(){
            InitializeComponent();
        }

        private void btnViewSourceCode_Click(object sender, EventArgs e){
            Process.Start("https://github.com/chylex/Code-Statistics");
        }
    }
}
