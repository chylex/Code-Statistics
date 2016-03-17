using System;
using System.Collections.Generic;
using CodeStatistics.Handling.Languages.Java.Utils;
using CodeStatistics.Handling.Utils;
using System.Linq;

namespace CodeStatistics.Handling.Languages.Java{
    public class JavaCodeBlockParser : CodeParser{
        private static readonly string[] ParsedKeywords = {
            "if", "else", "for", "while", "do", "switch", "case", "default", "try", "catch", "finally", "return", "break", "continue"
        };

        private static readonly HashSet<char> ParsedKeywordStarts = new HashSet<char>(ParsedKeywords.Select(str => str[0]).Distinct().ToList());
        private static readonly Predicate<char> IsKeywordStart = ParsedKeywordStarts.Contains;

        public JavaCodeBlockParser(string code) : base(code){
            IsWhiteSpace = JavaCharacters.IsWhiteSpace;
            IsIdentifierStart = JavaCharacters.IsIdentifierStart;
            IsIdentifierPart = JavaCharacters.IsIdentifierPart;
            IsValidIdentifier = str => str.Length > 0;
        }

        /// <summary>
        /// Skips until it hits a valid keyword surrounded by non-identifier characters. The cursor is placed right after the keyword.
        /// If no keyword is found, the cursor does not move and the method returns <see cref="string.Empty"/>, otherwise it returns the keyword.
        /// </summary>
        public string ReadNextKeywordSkip(){
            int prevCursor = cursor;

            while(!IsEOF){
                if (IsKeywordStart(Char) && (cursor == 0 || !IsIdentifierPart(code[cursor-1]))){
                    string identifier = ReadIdentifier();
                    if (identifier.Length > 0 && ParsedKeywords.Contains(identifier))return identifier;
                }
                
                Skip();
            }

            cursor = prevCursor;
            return string.Empty;
        }
    }
}
