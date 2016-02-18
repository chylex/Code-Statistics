﻿using System;
using System.IO;
using System.Text;

namespace CodeStatistics.Input{
    struct File{
        public readonly string FullPath;
        public readonly string Ext;

        private string[] _contents;

        public string[] Contents{
            get {
                if (_contents.Length == 0){
                    _contents = System.IO.File.ReadAllText(FullPath,Encoding.UTF8).Replace(@"\r","").TrimEnd().Split('\n');
                }

                return _contents;
            }
        }

        public File(string fullPath){
            if (fullPath == null)throw new ArgumentNullException("fullPath");

            this.FullPath = fullPath;
            this.Ext = Path.GetExtension(fullPath).Replace(".","").ToLowerInvariant();

            this._contents = new string[0];
        }

        public override bool Equals(object obj){
            return obj is File && ((File)obj).FullPath.Equals(this.FullPath);
        }

        public override int GetHashCode(){
            return FullPath.GetHashCode();
        }
    }
}
