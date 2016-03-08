using CodeStatistics.Input;
using System.Collections.Generic;
using CodeStatistics.Handling.Languages.Java.Elements;
using CodeStatistics.Handling.Languages.Java.Utils;
using CodeStatistics.Collections;

namespace CodeStatistics.Handling.Languages.Java{
    class JavaState{
        private readonly Dictionary<File,JavaFileInfo> fileInfo = new Dictionary<File,JavaFileInfo>();
        private readonly HashSet<string> packages = new HashSet<string>();

        public readonly CounterDictionary<string> AnnotationUses = new CounterDictionary<string>(8);
        public readonly CounterDictionary<string> FieldTypes = new CounterDictionary<string>(10);
        public readonly CounterDictionary<string> MethodReturnTypes = new CounterDictionary<string>(10);
        public readonly CounterDictionary<string> MethodParameterTypes = new CounterDictionary<string>(10);

        public readonly TopElementList<TypeIdentifier> IdentifiersSimpleTop = new TopElementList<TypeIdentifier>(10,(x,y) => y.Name.Length-x.Name.Length);
        public readonly TopElementList<TypeIdentifier> IdentifiersSimpleBottom = new TopElementList<TypeIdentifier>(10,(x,y) => x.Name.Length-y.Name.Length);
        public readonly TopElementList<TypeIdentifier> IdentifiersFullTop = new TopElementList<TypeIdentifier>(10,(x,y) => y.FullName.Length-x.FullName.Length);
        public readonly TopElementList<TypeIdentifier> IdentifiersFullBottom = new TopElementList<TypeIdentifier>(10,(x,y) => x.FullName.Length-y.FullName.Length);

        public int PackageCount { get { return packages.Count; } }

        public JavaFileInfo GetFile(File file){
            return fileInfo[file];
        }

        public JavaFileInfo Process(File file){
            JavaFileInfo info = new JavaFileInfo();
            fileInfo.Add(file,info);

            JavaCodeParser parser = new JavaCodeParser(JavaParseUtils.PrepareCodeFile(file.Contents));
            parser.AnnotationCallback += IncrementAnnotation;
            parser.CodeBlockCallback += blockParser => ReadCodeBlock(blockParser,info);

            ReadPackage(parser,info);
            ReadImportList(parser,info);
            ReadTopLevelTypes(parser,info);

            UpdateLocalData(info);

            return info;
        }

        private void UpdateLocalData(JavaFileInfo info){
            packages.Add(info.Package);

            foreach(Type type in info.Types){
                UpdateLocalDataForType(type);
            }
        }

        private void UpdateLocalDataForType(Type type){
            foreach(Type nestedType in type.NestedTypes){
                UpdateLocalDataForType(nestedType);
            }

            foreach(Field field in type.GetData().Fields){
                FieldTypes.Increment(field.Type.ToStringGeneral());
            }

            foreach(Method method in type.GetData().Methods){
                MethodReturnTypes.Increment(method.ReturnType.ToStringGeneral());

                foreach(TypeOf parameterType in method.ParameterTypes){
                    MethodParameterTypes.Increment(parameterType.ToStringGeneral());
                }
            }
        }

        private void IncrementAnnotation(Annotation annotation){
            AnnotationUses.Increment(annotation.SimpleName);
        }

        private static void ReadPackage(JavaCodeParser parser, JavaFileInfo info){
            parser.SkipSpaces();
            parser.SkipReadAnnotationList();
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

                Type type = parser.ReadType();

                if (type != null)info.Types.Add(type);
                else break;
            }
        }

        private static void ReadCodeBlock(JavaCodeParser blockParser, JavaFileInfo info){
            // TODO
        }
    }
}
