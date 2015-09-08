using System.IO;

namespace CodeStatistics.Input{
    struct File{
        public string FullName;
        public string Ext;

        public File(string FullName){
            this.FullName = Path.GetFileName(FullName);
            this.Ext = Path.GetExtension(FullName);
        }

        public override bool Equals(object obj){
            return obj is File && ((File)obj).FullName.Equals(this.FullName);
        }

        public override int GetHashCode(){
            return FullName.GetHashCode();
        }
    }
}
