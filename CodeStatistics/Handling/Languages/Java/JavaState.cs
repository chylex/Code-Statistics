using CodeStatistics.Input;
using System.Collections.Generic;

namespace CodeStatistics.Handling.Languages.Java{
    class JavaState{
        private readonly Dictionary<File,JavaFileInfo> fileInfo = new Dictionary<File,JavaFileInfo>();

        public JavaFileInfo Process(File file){
            JavaFileInfo info = new JavaFileInfo();
            fileInfo.Add(file,info);

            // TODO

            return info;
        }

        public JavaFileInfo GetFile(File file){
            return fileInfo[file];
        }
    }
}
