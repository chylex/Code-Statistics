﻿using CodeStatistics.Handling.Languages.Java.Elements;
using CodeStatistics.Handling.Utils;
using System.Text;

namespace CodeStatistics.Handling.Languages.Java{
    public class JavaCodeParser : CodeParser{
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
        /// 
        /// </summary>
        public string ReadPackageDeclaration(){
            SkipSpaces();

            return ""; // TODO
        }
    }
}
