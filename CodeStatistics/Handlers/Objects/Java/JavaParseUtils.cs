using CodeStatistics.Handlers.Objects.Java.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeStatistics.Handlers.Objects.Java{
    static class JavaParseUtils{
        public static readonly Regex commentOneLine = new Regex(@"//.*?$",RegexOptions.Compiled | RegexOptions.Multiline); // use for the whole string
        public static readonly Regex commentMultiLine = new Regex(@"/\*(?:.|\n)*?\*/",RegexOptions.Compiled); // use for the whole string
        public static readonly Regex strings = new Regex(@"([""']).*?\1",RegexOptions.Compiled); // verbatim strings with quotes need "" for literal
        
        public static readonly string[] typeIdentifiersSpace = new string[]{ "class ", "@interface ", "interface ", "enum " };
        public static readonly string[] modifiers = new string[]{ "public", "protected", "private", "static", "final", "abstract", "synchronized", "volatile", "native", "transient", "strictfp" };

        /// <summary>
        /// Checks whether the identifier consists of letters, numbers, $ and _.
        /// If you are using stuff like diacritics, ₯, ௹, or  ⁀  in java identifiers, stop.
        /// </summary>
        public static bool IsJavaIdentifier(string text){
            for(int ind = 0; ind < text.Length; ind++){
                char chr = text[ind];
                if (!char.IsLetter(chr) && !(char.IsDigit(chr) && ind > 0) && chr != '_' && chr != '$')return false;
            }

            return true;
        }

        /// <summary>
        /// Removes modifiers from the line, including the space after them.
        /// </summary>
        public static string StripModifiers(string line){
            string[] found;
            return StripModifiers(line,out found);
        }

        /// <summary>
        /// Removes modifiers from the line, including the space after them, and returns all found modifiers.
        /// </summary>
        public static string StripModifiers(string line, out string[] found){
            List<string> foundModifiers = new List<string>();

            for(int modifierInd = 0, index; modifierInd < modifiers.Length; modifierInd++){
                string modifier = modifiers[modifierInd];

                if ((index = line.IndexOf(modifier)) != -1){
                    foundModifiers.Add(modifier);
                    line = line.Remove(index,modifier.Length+1); // remove space
                }
            }

            found = foundModifiers.ToArray();
            return line;
        }

        /// <summary>
        /// Finds the java type and its name in the line. If the found text is not a valid java identifier, a KeyValuePair with JavaType.Invalid is returned.
        /// </summary>
        public static KeyValuePair<JavaType,string> GetType(string line){
            line = StripModifiers(line.RemoveFrom(" extends").RemoveFrom(" implements").ExtractEnd("{")).TrimStart();

            string type = typeIdentifiersSpace.FirstOrDefault(identifier => line.StartsWith(identifier));

            if (type != null && IsJavaIdentifier(line = line.Substring(type.Length).Trim())){
                JavaType javaType = type.Equals("class ") ? JavaType.Class :
                                    type.Equals("interface ") ? JavaType.Interface :
                                    type.Equals("enum ") ? JavaType.Enum :
                                    type.Equals("@interface ") ? JavaType.Annotation : JavaType.Invalid;
                return new KeyValuePair<JavaType,string>(javaType,line);
            }
            else return new KeyValuePair<JavaType,string>(JavaType.Invalid,null);
        }

        /// <summary>
        /// Returns the simple name (all text after the last dot).
        /// </summary>
        public static KeyValuePair<string,string> GetSimpleName(string fullName){
            int index = fullName.LastIndexOf('.');
            return new KeyValuePair<string,string>(index == -1 ? fullName : fullName.Substring(index+1),index == -1 ? "" : fullName.Substring(0,index+1));
        }
    }
}
