using System.IO;
using System.Text;

namespace CodeStatistics.Input{
    struct File{
        public readonly string FullPath;
        public readonly string Ext;

        public File(string fullPath){
            this.FullPath = fullPath;
            this.Ext = Path.GetExtension(fullPath).Replace(".","");
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
