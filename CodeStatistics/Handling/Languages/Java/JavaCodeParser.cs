using CodeStatistics.Handling.Utils;

namespace CodeStatistics.Handling.Languages.Java{
    class JavaCodeParser : CodeParser{
        public JavaCodeParser(string code) : base(code){
            IsWhiteSpace = JavaCharacters.IsWhiteSpace;
            IsIdentifierStart = JavaCharacters.IsIdentifierStart;
            IsIdentifierPart = JavaCharacters.IsIdentifierPart;
            IsValidIdentifier = JavaCharacters.IsNotReservedWord;
        }
    }
}
