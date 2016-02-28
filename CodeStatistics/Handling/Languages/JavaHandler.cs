using System;
using System.IO;
using System.Linq;
using CodeStatistics.Handling.Utils;
using File = CodeStatistics.Input.File;
using CodeStatistics.Handling.Languages.Java;
using CodeStatistics.Handling.Languages.Java.Utils;
using Type = CodeStatistics.Handling.Languages.Java.Elements.Type;
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
                ProcessType(info.Package,type,variables);
            }
        }

        private void ProcessType(string prefix, Type type, Variables.Root variables){
            foreach(Type nestedType in type.NestedTypes){
                ProcessType(prefix+"."+type.Identifier,nestedType,variables);
            }

            variables.Increment("javaTypesTotal");

            switch(type.Declaration){
                case Type.DeclarationType.Class: variables.Increment("javaClasses"); break;
                case Type.DeclarationType.Interface: variables.Increment("javaInterfaces"); break;
                case Type.DeclarationType.Enum: variables.Increment("javaEnums"); break;
                case Type.DeclarationType.Annotation: variables.Increment("javaAnnotations"); break;
            }
            
            TypeIdentifier identifier = new TypeIdentifier(prefix,type.Identifier);

            int simpleNameLength = identifier.Name.Length;
            variables.Minimum("javaNamesSimpleMin",simpleNameLength);
            variables.Maximum("javaNamesSimpleMax",simpleNameLength);
            variables.Increment("javaNamesSimpleTotal",simpleNameLength);

            int fullLength = identifier.FullName.Length;
            variables.Minimum("javaNamesFullMin",fullLength);
            variables.Maximum("javaNamesFullMax",fullLength);
            variables.Increment("javaNamesFullTotal",fullLength);

            JavaState state = variables.GetStateObject<JavaState>(this);
            state.IdentifiersSimpleTop.Add(identifier);
            state.IdentifiersSimpleBottom.Add(identifier);
            state.IdentifiersFullTop.Add(identifier);
            state.IdentifiersFullBottom.Add(identifier);
        }

        public override void FinalizeProject(Variables.Root variables){
            base.FinalizeProject(variables);

            JavaState state = variables.GetStateObject<JavaState>(this);

            variables.SetVariable("javaPackages",state.PackageCount);

            variables.Average("javaImportsAvg","javaImportsTotal","javaCodeFiles");
            variables.Average("javaNamesSimpleAvg","javaNamesSimpleTotal","javaTypesTotal");
            variables.Average("javaNamesFullAvg","javaNamesFullTotal","javaTypesTotal");

            foreach(TypeIdentifier identifier in state.IdentifiersSimpleTop){
                variables.AddToArray("javaNamesSimpleTop",new { package = identifier.Prefix, name = identifier.Name });
            }

            foreach(TypeIdentifier identifier in state.IdentifiersSimpleBottom.Reverse()){
                variables.AddToArray("javaNamesSimpleBottom",new { package = identifier.Prefix, name = identifier.Name });
            }

            foreach(TypeIdentifier identifier in state.IdentifiersFullTop){
                variables.AddToArray("javaNamesFullTop",new { package = identifier.Prefix, name = identifier.Name });
            }

            foreach(TypeIdentifier identifier in state.IdentifiersFullBottom.Reverse()){
                variables.AddToArray("javaNamesFullBottom",new { package = identifier.Prefix, name = identifier.Name });
            }
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
