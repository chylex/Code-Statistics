using System.Collections.Generic;
using CodeStatistics.Data;
using CodeStatistics.Properties;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;
using System;

namespace CodeStatistics.Forms{
    public sealed partial class AboutForm : Form{
        private static Dictionary<string,string> AboutFormData{
            get{
                return new Dictionary<string,string>{
                    { "[version]", Application.ProductVersion },
                    { "[company]", Application.CompanyName }
                };
            }
        }

        public AboutForm(){
            InitializeComponent();
#if !MONO
            this.HelpButton = true; // Mono does not support OnHelpButtonClicked
            this.HelpButtonClicked += OnHelpButtonClicked;
#endif
            Text = Lang.Get["TitleAbout"];
            btnReadme.Text = Lang.Get["AboutReadme"];
            textContents.Rtf = AboutFormData.Aggregate(Resources.about,(contents, kvp) => contents.Replace(kvp.Key,kvp.Value));
        }
#if !MONO
        private void OnHelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e){
            e.Cancel = true;
            Process.Start("https://github.com/chylex/Code-Statistics/issues");
        }
#endif
        private void btnReadme_Click(object sender, EventArgs e){
            Process.Start("https://github.com/chylex/Code-Statistics/blob/master/README.md");
        }
    }
}
