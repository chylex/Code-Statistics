using System.IO;
using CodeStatistics.Handling.Utils;
using File = CodeStatistics.Input.File;
using CodeStatistics.Handling.Languages.Java;
using CodeStatistics.Handling.Languages.Java.Utils;
using CodeStatistics.Handling.Languages.Java.Elements;

namespace CodeStatistics.Handling.Languages{
    class JavaHandler : AbstractLanguageFileHandler{
        public override int Weight{
            get { return 50; }
        }

        protected override string Key{
            get { return "java"; }
        }

        public override void SetupProject(Variables.Root variables){
            base.SetupProject(variables);
            variables.AddStateObject(this,new JavaState());
        }

        public override void Process(File file, Variables.Root variables){
            base.Process(file,variables);

            JavaState state = variables.GetStateObject<JavaState>(this);
            JavaFileInfo info = state.Process(file);

            variables.Increment("javaImportsTotal",info.Imports.Count);
            variables.Maximum("javaImportsMax",info.Imports.Count);

            foreach(Type type in info.Types){
                ProcessType(type,variables);
            }
        }

        private void ProcessType(Type type, Variables.Root variables){
            foreach(Type nestedType in type.NestedTypes){
                ProcessType(nestedType,variables);
            }

            switch(type.Declaration){
                case Type.DeclarationType.Class: variables.Increment("javaClasses"); break;
                case Type.DeclarationType.Interface: variables.Increment("javaInterfaces"); break;
                case Type.DeclarationType.Enum: variables.Increment("javaEnums"); break;
                case Type.DeclarationType.Annotation: variables.Increment("javaAnnotations"); break;
            }
        }

        public override void FinalizeProject(Variables.Root variables){
            base.FinalizeProject(variables);

            JavaState state = variables.GetStateObject<JavaState>(this);

            variables.SetVariable("javaPackages",state.PackageCount);

            variables.Average("javaImportsAvg","javaImportsTotal","javaCodeFiles");
        }

        protected override object GetFileObject(FileIntValue fi, Variables.Root variables){
            JavaState state = variables.GetStateObject<JavaState>(this);
            return new { package = state.GetFile(fi.File).Package.Replace('.','/')+'/', file = Path.GetFileName(fi.File.FullPath), amount = fi.Value };
        }

        public override string PrepareFileContents(string contents){
            return JavaParseUtils.PrepareCodeFile(contents);
        }
    }
}
