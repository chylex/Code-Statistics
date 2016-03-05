using System;
using System.IO;
using System.Linq;
using CodeStatistics.Handling.Utils;
using File = CodeStatistics.Input.File;
using CodeStatistics.Handling.Languages.Java;
using CodeStatistics.Handling.Languages.Java.Utils;
using Type = CodeStatistics.Handling.Languages.Java.Elements.Type;
using CodeStatistics.Handling.Languages.Java.Elements;
using System.Collections.Generic;

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

            // declaration type
            variables.Increment("javaTypesTotal");

            string declPrefix = "javaUnknown";

            switch(type.Declaration){
                case Type.DeclarationType.Class: variables.Increment(declPrefix = "javaClasses"); break;
                case Type.DeclarationType.Interface: variables.Increment(declPrefix = "javaInterfaces"); break;
                case Type.DeclarationType.Enum: variables.Increment(declPrefix = "javaEnums"); break;
                case Type.DeclarationType.Annotation: variables.Increment(declPrefix = "javaAnnotations"); break;
            }
            
            // identifier
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

            // fields
            List<Field> fields = type.GetData().Fields;
            int fieldsDefault = fields.Count;

            variables.Increment(declPrefix+"FieldsTotal",fields.Count);
            variables.Minimum(declPrefix+"FieldsMin",fields.Count);
            variables.Maximum(declPrefix+"FieldsMax",fields.Count);

            foreach(Modifiers modifier in JavaModifiers.Values){
                int count = fields.Count(field => field.Modifiers.HasFlag(modifier));
                if (modifier == Modifiers.Public || modifier == Modifiers.Protected || modifier == Modifiers.Private)fieldsDefault -= count;

                variables.Increment(declPrefix+"Fields"+JavaModifiers.ToString(modifier).CapitalizeFirst(),count);
            }

            variables.Increment(declPrefix+"FieldsDefaultVisibility",fieldsDefault);

            // methods
            List<Method> constructorMethods = type.GetData().Methods.Where(method => method.IsConstructor).ToList();
            List<Method> classMethods = type.GetData().Methods.Where(method => !method.IsConstructor).ToList();

            int methodsDefault = classMethods.Count;
            
            variables.Increment(declPrefix+"ConstructorsTotal",constructorMethods.Count);
            variables.Increment(declPrefix+"MethodsTotal",classMethods.Count);
            variables.Minimum(declPrefix+"MethodsMin",classMethods.Count);
            variables.Maximum(declPrefix+"MethodsMax",classMethods.Count);

            foreach(Modifiers modifier in JavaModifiers.Values){
                int count = classMethods.Count(method => method.Modifiers.HasFlag(modifier));
                if (modifier == Modifiers.Public || modifier == Modifiers.Protected || modifier == Modifiers.Protected)methodsDefault -= count;

                variables.Increment(declPrefix+"Methods"+JavaModifiers.ToString(modifier).CapitalizeFirst(),count);
            }

            variables.Increment(declPrefix+"MethodsDefaultVisibility",methodsDefault);
        }

        public override void FinalizeProject(Variables.Root variables){
            base.FinalizeProject(variables);

            JavaState state = variables.GetStateObject<JavaState>(this);

            // general
            variables.SetVariable("javaPackages",state.PackageCount);

            // imports
            variables.Average("javaImportsAvg","javaImportsTotal","javaCodeFiles");
            variables.Average("javaNamesSimpleAvg","javaNamesSimpleTotal","javaTypesTotal");
            variables.Average("javaNamesFullAvg","javaNamesFullTotal","javaTypesTotal");

            // identifiers
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

            // fields and methods
            variables.Average("javaClassesFieldsAvg","javaClassesFieldsTotal","javaClasses");
            variables.Average("javaClassesConstructorsAvg","javaClassesConstructorsTotal","javaClasses");
            variables.Average("javaClassesMethodsAvg","javaClassesMethodsTotal","javaClasses");
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
