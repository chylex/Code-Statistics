using System.IO;
using System.Text;

namespace CodeStatistics.Input{
    struct File{
        public string FullPath;
        public string Ext;

        public File(string FullPath){
            this.FullPath = FullPath;
            this.Ext = Path.GetExtension(FullPath).Replace(".","");
        }

        public string Read(){
            return System.IO.File.ReadAllText(FullPath,Encoding.UTF8).Replace("\\r","");
        }

        public override bool Equals(object obj){
            return obj is File && ((File)obj).FullPath.Equals(this.FullPath);
        }

        public override int GetHashCode(){
            return FullPath.GetHashCode();
        }
    }
}
