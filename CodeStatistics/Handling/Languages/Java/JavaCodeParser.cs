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
    }
}
