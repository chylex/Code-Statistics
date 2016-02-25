﻿using System.Collections.Generic;
using CodeStatistics.Handling.Languages.Java.Elements;
using CodeStatistics.Handling.Utils;
using System.Text;
using CodeStatistics.Handling.Languages.Java.Utils;

namespace CodeStatistics.Handling.Languages.Java{
    public class JavaCodeParser : CodeParser{
        public JavaCodeParser(string code) : base(code){
            IsWhiteSpace = JavaCharacters.IsWhiteSpace;
            IsIdentifierStart = JavaCharacters.IsIdentifierStart;
            IsIdentifierPart = JavaCharacters.IsIdentifierPart;
            IsValidIdentifier = JavaCharacters.IsNotReservedWord;
        }

        public override CodeParser Clone(string newCode = null){
            return new JavaCodeParser(newCode ?? string.Empty);
        }

        /// <summary>
        /// Skips to the next matching character where the brackets ([{ }]) are balanced, and returns itself.
        /// If the skip fails, the cursor will not move.
        /// </summary>
        public JavaCodeParser SkipToIfBalanced(char chr){
            int prevCursor = cursor;
            Stack<char> bracketStack = new Stack<char>(4);

            while(cursor < length){
                if (bracketStack.Count == 0 && Char == chr)break;

                switch(Char){
                    case '(': bracketStack.Push(')'); break;
                    case '[': bracketStack.Push(']'); break;
                    case '{': bracketStack.Push('}'); break;

                    case ')': case ']': case '}':
                        if (bracketStack.Count == 0 || bracketStack.Pop() != Char){
                            cursor = prevCursor;
                            return this;
                        }

                        break;
                }

                ++cursor;
            }

            if (Char != chr || bracketStack.Count > 0)cursor = prevCursor;

            return this;
        }

        /// <summary>
        /// Skips to the next matching character where the brackets ([{ }]) are balanced, and returns a new instance of JavaCodeParser with the contents
        /// of all skipped characters. If the skip fails, the returned contents will be empty and the cursor will not move.
        /// </summary>
        public JavaCodeParser ReadToIfBalanced(char chr){
            int indexStart = cursor;
            SkipToIfBalanced(chr);
            return (JavaCodeParser)Clone(SubstrIndex(indexStart,cursor));
        }

        /// <summary>
        /// Reads the entire full type name, which consists of one or more identifiers separated by the dot character. <para/>
        /// https://docs.oracle.com/javase/specs/jls/se8/html/jls-6.html#d5e7695
        /// </summary>
        public string ReadFullTypeName(){
            StringBuilder build = new StringBuilder();

            string identifier = ReadIdentifier();
            if (identifier.Length == 0)return string.Empty;

            while(true){
                build.Append(identifier);

                if (Char == '.'){
                    build.Append('.');

                    identifier = Skip().ReadIdentifier();
                    if (identifier.Length == 0)return string.Empty;
                }
                else break;
            }

            return build.ToString();
        }

        /// <summary>
        /// https://docs.oracle.com/javase/specs/jls/se8/html/jls-9.html#jls-9.7
        /// </summary>
        public Annotation? ReadAnnotation(){
            if (Char != '@')return null;

            Skip().SkipSpaces(); // skip @ and spaces

            string simpleName = JavaParseUtils.FullToSimpleName(ReadFullTypeName()); // read type name
            if (simpleName.Length == 0)return null;

            if (SkipSpaces().Char == '('){ // skip arguments and ignore
                SkipBlock('(',')');
            }
            
            return new Annotation(simpleName);
        }

        /// <summary>
        /// Skips spaces and finds all following annotations.
        /// </summary>
        public List<Annotation> SkipReadAnnotationList(){
            return JavaParseUtils.ReadStructList(this,ReadAnnotation,1);
        }

        /// <summary>
        /// Reads the package declaration (excluding package modifier - that has to be read separately).
        /// https://docs.oracle.com/javase/specs/jls/se8/html/jls-7.html#jls-7.4
        /// </summary>
        public string ReadPackageDeclaration(){
            return SkipIfMatch("package^s") ? ((JavaCodeParser)ReadToSkip(';')).ReadFullTypeName() : string.Empty;
        }

        /// <summary>
        /// https://docs.oracle.com/javase/specs/jls/se8/html/jls-7.html#jls-7.5
        /// </summary>
        public Import? ReadImportDeclaration(){
            if (!SkipIfMatch("import^s"))return null;

            bool isStatic = SkipIfMatch("static^s");

            string type = ((JavaCodeParser)ReadToSkip(';')).ReadFullTypeName();
            if (type.Length == 0)return null;

            return new Import(type,isStatic);
        }

        /// <summary>
        /// Reads a modifier specified in <see cref="Modifiers"/> and skips it.
        /// </summary>
        public Modifiers? ReadModifier(){
            foreach(string modifierStr in JavaModifiers.Strings){
                if (SkipIfMatch(modifierStr+"^s")){
                    return JavaModifiers.FromString(modifierStr);
                }
            }

            return null;
        }

        /// <summary>
        /// Skips spaces and finds all following modifiers.
        /// </summary>
        public List<Modifiers> SkipReadModifierList(){
            return JavaParseUtils.ReadStructList(this,ReadModifier,2);
        }

        /// <summary>
        /// Reads a primitive value specified in <see cref="Primitives"/> and skips it.
        /// </summary>
        public Primitives? ReadPrimitive(){
            foreach(string primitiveStr in JavaPrimitives.Strings){
                if (SkipIfMatch(primitiveStr+"^s")){
                    return JavaPrimitives.FromString(primitiveStr);
                }
            }

            return null;
        }

        /// <summary>
        /// Skips spaces and reads following member info (list of annotations and modifiers).
        /// </summary>
        public Member SkipReadMemberInfo(){
            return new Member(SkipReadAnnotationList(),SkipReadModifierList());
        }

        /// <summary>
        /// Skips spaces and all following pairs of angled and square brackets.
        /// </summary>
        public JavaCodeParser SkipTypeArrayAndGenerics(){
            do{
                SkipSpaces();
                if (Char == '[')SkipBlock('[',']');
                if (Char == '<')SkipBlock('<','>');
            }
            while(!IsEOF && (Char == '[' || Char == '<'));

            return this;
        }

        /// <summary>
        /// Reads the type, which can either be a method return type or a field type, and skips it.
        /// </summary>
        public TypeOf? ReadTypeOf(){
            if (SkipIfMatch("void^s"))return TypeOf.Void();

            // primitive
            Primitives? primitive = ReadPrimitive();

            if (primitive.HasValue){
                SkipTypeArrayAndGenerics();
                return TypeOf.Primitive(primitive.Value);
            }

            // object name
            string typeName = ReadFullTypeName();

            if (typeName.Length > 0){
                SkipTypeArrayAndGenerics();
                return TypeOf.Object(JavaParseUtils.FullToSimpleName(typeName));
            }

            // nothing
            return null;
        }

        /// <summary>
        /// Reads a declaration type specified in <see cref="Type.DeclarationType"/> and skips it.
        /// </summary>
        public Type.DeclarationType? ReadTypeDeclaration(){
            if (SkipIfMatch("class^s"))return Type.DeclarationType.Class;
            else if (SkipIfMatch("interface^s"))return Type.DeclarationType.Interface;
            else if (SkipIfMatch("enum^s"))return Type.DeclarationType.Enum;
            else if (SkipIfMatch("@interface^s"))return Type.DeclarationType.Annotation;
            else return null;
        }

        /// <summary>
        /// Reads an entire type declaration and generates data from the contents, and skips the block.
        /// </summary>
        public Type ReadType(){
            Member memberInfo = SkipReadMemberInfo();

            Type.DeclarationType? type = ReadTypeDeclaration();
            if (!type.HasValue)return null;

            string identifier = SkipSpaces().ReadIdentifier();
            if (identifier.Length == 0)return null;

            Type readType = new Type(type.Value,identifier,memberInfo);
            ((JavaCodeParser)SkipTo('{').ReadBlock('{','}')).ReadTypeContents(readType);

            return readType;
        }

        /// <summary>
        /// Recursively reads all members of a Type, including all nested Types. Called on a cloned JavaCodeParser that only
        /// contains contents of the Type block.
        /// </summary>
        private void ReadTypeContents(Type type){
            if (type.Declaration == Type.DeclarationType.Enum){
                return;
                // TODO
            }

            int skippedMembers = 0;

            while(!IsEOF && skippedMembers < 50){
                Member memberInfo = SkipReadMemberInfo(); // skips spaces at the beginning and end

                // nested types
                Type.DeclarationType? declaration = ReadTypeDeclaration();

                if (declaration.HasValue){
                    string identifier = SkipSpaces().ReadIdentifier();
                    if (identifier.Length == 0)break; // TODO handle error

                    Type nestedType = new Type(declaration.Value,identifier,memberInfo);
                    ((JavaCodeParser)SkipTo('{').ReadBlock('{','}')).ReadTypeContents(nestedType);

                    type.NestedTypes.Add(nestedType);
                    continue;
                }

                // static / instance initializer
                if (Char == '{'){
                    Method method = new Method(memberInfo.Modifiers.HasFlag(Modifiers.Static) ? "<clinit>" : "<init>",TypeOf.Void(),memberInfo);
                    SkipBlock('{','}');

                    type.GetData().Methods.Add(method);
                    continue;
                }

                // fields and methods
                if (Char == '<'){
                    SkipBlock('<','>');
                    SkipSpaces();
                }

                TypeOf? returnOrFieldType = ReadTypeOf();

                if (returnOrFieldType.HasValue){
                    string identifier = SkipSpaces().ReadIdentifier();
                    
                    if (string.Equals(returnOrFieldType.Value.AsSimpleType(),type.Identifier)){ // constructor
                        identifier = "<init>";
                        returnOrFieldType = TypeOf.Void();
                    }

                    if (identifier.Length == 0)break; // TODO handle error

                    if (SkipSpaces().Char == '('){ // method
                        List<TypeOf> parameterList = ((JavaCodeParser)ReadBlock('(',')')).ReadMethodParameterList();
                        Method method = new Method(identifier,returnOrFieldType.Value,parameterList,memberInfo);
                        SkipBlock('{','}');

                        type.GetData().Methods.Add(method);
                    }
                    else{ // field
                        // TODO
                        SkipToIfBalanced(';').Skip();
                    }

                    continue;
                }

                // skip
                SkipBlock('{','}');
                SkipSpaces();
                ++skippedMembers;
            }

            if (skippedMembers == 50){
                System.Diagnostics.Debug.WriteLine("Caution: Skipped 50 members."); // TODO
            }
        }

        /// <summary>
        /// Reads all parameters of a method. Called on a cloned JavaCodeParser that only contains contents between parentheses.
        /// </summary>
        private List<TypeOf> ReadMethodParameterList(){
            var list = new List<TypeOf>();
            if (SkipSpaces().IsEOF)return list;

            do{
                SkipSpaces();

                TypeOf? type = ReadTypeOf();
                if (!type.HasValue)break;

                list.Add(type.Value);
                SkipTo(',');
            }while(Char == ',');

            return list;
        }
    }
}
