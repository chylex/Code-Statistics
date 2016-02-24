using System.Collections.Generic;
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
            return SkipIfMatch("package^s") ? ReadToSkip(';').Contents : string.Empty;
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
        /// Skips spaces and reads following member info (list of annotations and modifiers).
        /// </summary>
        public Member SkipReadMemberInfo(){
            return new Member(SkipReadAnnotationList(),SkipReadModifierList());
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

        private void ReadTypeContents(Type type){
            // TODO
        }
    }
}
