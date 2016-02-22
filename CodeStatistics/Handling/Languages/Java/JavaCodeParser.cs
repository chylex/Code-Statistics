using CodeStatistics.Handling.Languages.Java.Elements;
using CodeStatistics.Handling.Utils;
using System.Text;

namespace CodeStatistics.Handling.Languages.Java{
    class JavaCodeParser : CodeParser{
        public JavaCodeParser(string code) : base(code){
            IsWhiteSpace = JavaCharacters.IsWhiteSpace;
            IsIdentifierStart = JavaCharacters.IsIdentifierStart;
            IsIdentifierPart = JavaCharacters.IsIdentifierPart;
            IsValidIdentifier = JavaCharacters.IsNotReservedWord;
        }

        /// <summary>
        /// Reads the entire full type name, which consists of one or more identifiers separated by the dot character. <para/>
        /// https://docs.oracle.com/javase/specs/jls/se8/html/jls-6.html#d5e7695
        /// </summary>
        public string ReadFullTypeName(){
            StringBuilder build = new StringBuilder();

            string identifier = NextIdentifier();
            if (identifier.Length == 0)return string.Empty;

            while(true){
                build.Append(identifier);

                if (Char == '.'){
                    build.Append('.');

                    identifier = Skip().NextIdentifier();
                    if (identifier.Length == 0)return string.Empty;
                }
                else break;
            }

            return build.ToString();
        }
    }
}
