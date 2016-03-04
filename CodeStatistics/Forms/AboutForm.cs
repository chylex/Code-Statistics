using CodeStatistics.Data;
using CodeStatistics.Properties;
using System.Windows.Forms;

namespace CodeStatistics.Forms{
    public partial class AboutForm : Form{
        public AboutForm(){
            InitializeComponent();

            Text = Lang.Get["TitleAbout"];
            textContents.Rtf = Resources.about;
        }
    }
}
