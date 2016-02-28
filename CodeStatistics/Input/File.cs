using System;
using System.IO;
using System.Text;

namespace CodeStatistics.Input{
    public struct File{
        public readonly string FullPath;
        public readonly string Ext;

        private string _contents;

        public string Contents{
            get {
                if (_contents == "\0"){
                    _contents = System.IO.File.ReadAllText(FullPath,Encoding.UTF8).Replace(@"\r","").TrimEnd();
                }

                return _contents;
            }
        }

        public File(string fullPath){
            if (fullPath == null)throw new ArgumentNullException("fullPath");

            this.FullPath = fullPath;
            this.Ext = Path.GetExtension(fullPath).Replace(".","").ToLowerInvariant();

            this._contents = "\0";
        }

        public override bool Equals(object obj){
            return obj is File && ((File)obj).FullPath.Equals(this.FullPath);
        }

        public override int GetHashCode(){
            return FullPath.GetHashCode();
        }

        public override string ToString(){
            return Path.GetFileName(FullPath);
        }
    }
}
