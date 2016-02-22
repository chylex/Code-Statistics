using CodeStatistics.Handling.Utils;
using CodeStatistics.Input;
using System.Collections.Generic;

namespace CodeStatistics.Handling.Languages.Java{
    class JavaState{
        private readonly Dictionary<File,JavaFileInfo> fileInfo = new Dictionary<File,JavaFileInfo>();

        public JavaFileInfo GetFile(File file){
            return fileInfo[file];
        }

        public JavaFileInfo Process(File file){
            JavaFileInfo info = new JavaFileInfo();
            fileInfo.Add(file,info);

            JavaCodeParser parser = new JavaCodeParser(file.Contents);

            // TODO

            return info;
        }
    }
}
