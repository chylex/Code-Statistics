using CodeStatistics.Input;
using System.Collections.Generic;
using CodeStatistics.Handling.Languages.Java.Elements;

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

            ReadPackage(parser,info);

            return info;
        }

        private void ReadPackage(JavaCodeParser parser, JavaFileInfo info){
            parser.SkipSpaces();

            Annotation? annotation = parser.ReadAnnotation();

            if (annotation.HasValue){
                // TODO
            }
            
            parser.SkipSpaces();
            info.Package = parser.ReadPackageDeclaration();
        }
    }
}
