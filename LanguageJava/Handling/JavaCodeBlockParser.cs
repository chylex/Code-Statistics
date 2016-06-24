using System;
using System.Collections.Generic;
using System.Linq;
using CodeStatisticsCore.Handling.Utils;
using LanguageJava.Utils;

namespace LanguageJava.Handling{
    public class JavaCodeBlockParser : CodeParser{
        private static readonly string[] ParsedKeywords = {
            "if", "else", "for", "while", "do", "switch", "case", "default", "try", "catch", "finally", "return", "break", "continue"
        };

        private static readonly HashSet<char> ParsedKeywordStarts = new HashSet<char>(ParsedKeywords.Select(str => str[0]).Distinct().ToList());
        private static readonly Predicate<char> IsKeywordStart = ParsedKeywordStarts.Contains;

        private int prevCursor;

        public JavaCodeBlockParser(string code) : base(code){
            IsWhiteSpace = JavaCharacters.IsWhiteSpace;
            IsIdentifierStart = JavaCharacters.IsIdentifierStart;
            IsIdentifierPart = JavaCharacters.IsIdentifierPart;
            IsValidIdentifier = str => str.Length > 0;
        }

        public override CodeParser Clone(string newCode = null){
            return new JavaCodeBlockParser(newCode ?? string.Empty);
        }

        /// <summary>
        /// Goes back to the last cursor position before calling <see cref="ReadNextKeywordSkip"/>. Returns itself.
        /// </summary>
        public JavaCodeBlockParser RevertKeywordSkip(){
            cursor = prevCursor;
            return this;
        }

        /// <summary>
        /// Skips until it hits a valid keyword surrounded by non-identifier characters. The cursor is placed right after the keyword.
        /// If no keyword is found, the cursor does not move and the method returns <see cref="string.Empty"/>, otherwise it returns the keyword.
        /// </summary>
        public string ReadNextKeywordSkip(){
            prevCursor = cursor;

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
