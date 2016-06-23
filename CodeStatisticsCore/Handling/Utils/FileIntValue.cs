using System;
using CodeStatisticsCore.Input;

namespace CodeStatisticsCore.Handling.Utils{
    public struct FileIntValue{
        public static readonly Comparison<FileIntValue> SortMax = (x, y) => y.Value-x.Value;
        public static readonly Comparison<FileIntValue> SortMin = (x, y) => x.Value-y.Value;

        public readonly File File;
        public readonly int Value;

        public FileIntValue(File file, int value){
            this.File = file;
            this.Value = value;
        }

        public override bool Equals(object obj){
            return File.Equals(obj);
        }

        public override int GetHashCode(){
            return File.GetHashCode();
        }
    }
}
