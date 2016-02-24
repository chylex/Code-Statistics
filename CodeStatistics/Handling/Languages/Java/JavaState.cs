using CodeStatistics.Input;
using System.Collections.Generic;
using CodeStatistics.Handling.Languages.Java.Elements;
using CodeStatistics.Handling.Languages.Java.Utils;

namespace CodeStatistics.Handling.Languages.Java{
    class JavaState{
        private readonly Dictionary<File,JavaFileInfo> fileInfo = new Dictionary<File,JavaFileInfo>();
        private readonly HashSet<string> packages = new HashSet<string>();

        public int PackageCount { get { return packages.Count; } }

        public JavaFileInfo GetFile(File file){
            return fileInfo[file];
        }

        public JavaFileInfo Process(File file){
            JavaFileInfo info = new JavaFileInfo();
            fileInfo.Add(file,info);

            JavaCodeParser parser = new JavaCodeParser(JavaParseUtils.PrepareCodeFile(file.Contents));

            ReadPackage(parser,info);
            ReadImportList(parser,info);
            ReadTopLevelTypes(parser,info);

            UpdateLocalData(info);

            return info;
        }

        private void UpdateLocalData(JavaFileInfo info){
            packages.Add(info.Package);
        }

        private static void ReadPackage(JavaCodeParser parser, JavaFileInfo info){
            parser.SkipSpaces();

            List<Annotation> annotations = ReadAnnotationList(parser);
            // TODO handle annotation list
            
            parser.SkipSpaces();
            info.Package = parser.ReadPackageDeclaration();
        }

        private static void ReadImportList(JavaCodeParser parser, JavaFileInfo info){
            while(true){
                parser.SkipSpaces();

                Import? import = parser.ReadImportDeclaration();
                if (!import.HasValue)break;

                info.Imports.Add(import.Value);
            }
        }

        private static void ReadTopLevelTypes(JavaCodeParser parser, JavaFileInfo info){
            while(true){
                parser.SkipSpaces();
                if (parser.IsEOF)break;

                Type type = ReadType(parser);

                if (type != null)info.Types.Add(type);
                else break;
            }
        }

        private static Type ReadType(JavaCodeParser parser){
            Member memberInfo = ReadMemberInfo(parser);

            Type.DeclarationType? type = parser.ReadTypeDeclaration();
            if (!type.HasValue)return null;

            string identifier = parser.SkipSpaces().ReadIdentifier();
            if (identifier.Length == 0)return null;

            Type readType = new Type(type.Value,identifier,memberInfo);
            ReadTypeContents((JavaCodeParser)parser.SkipTo('{').ReadBlock('{','}'),readType);
            return readType;
        }

        private static void ReadTypeContents(JavaCodeParser contents, Type type){
            // TODO
        }

        private static List<Annotation> ReadAnnotationList(JavaCodeParser parser){
            return JavaParseUtils.ReadStructList(parser,parser.ReadAnnotation,1);
        }

        private static List<Modifiers> ReadModifierList(JavaCodeParser parser){
            return JavaParseUtils.ReadStructList(parser,parser.ReadModifier,2);
        }

        private static Member ReadMemberInfo(JavaCodeParser parser){
            return new Member(ReadAnnotationList(parser),ReadModifierList(parser));
        }
    }
}
