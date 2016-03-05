using System.Collections.Generic;
using CodeStatistics.Data;
using CodeStatistics.Properties;
using System.Windows.Forms;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;

namespace CodeStatistics.Forms{
    public partial class AboutForm : Form{
        private static Dictionary<string,string> AboutFormData{
            get{
                return new Dictionary<string,string>{
                    { "[version]", Application.ProductVersion }
                };
            }
        }

        public AboutForm(){
            InitializeComponent();

            Text = Lang.Get["TitleAbout"];
            textContents.Rtf = AboutFormData.Aggregate(Resources.about,(contents, kvp) => contents.Replace(kvp.Key,kvp.Value));
        }

        private void OnHelpButtonClicked(object sender, CancelEventArgs e){
            e.Cancel = true;
            Process.Start("https://github.com/chylex/Code-Statistics/issues");
        }
    }
}
