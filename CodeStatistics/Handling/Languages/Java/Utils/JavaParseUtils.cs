using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CodeStatistics.Handling.Languages.Java.Utils{
    public static class JavaParseUtils{
        // verbatim strings with quotes need "" for literal
        private static readonly Regex RegexString = new Regex(@"([""'])(?:\\[\\'""btnfru0-7]|[^\\""])*?(?:\1|$|(?=\*/))",RegexOptions.Compiled | RegexOptions.Multiline);
        private static readonly Regex RegexCommentSingle = new Regex(@"//.*?$",RegexOptions.Compiled | RegexOptions.Multiline);
        private static readonly Regex RegexCommentMulti = new Regex(@"/\*.*?\*/",RegexOptions.Compiled | RegexOptions.Singleline);

        public delegate T? ReadStruct<T>() where T : struct;
        
        public static string PrepareCodeFile(string code){
            string processed = code;
            processed = RegexString.Replace(processed,@""""""); // beautiful
            processed = RegexCommentSingle.Replace(processed," ");
            processed = RegexCommentMulti.Replace(processed," ");
            return processed;
        }

        public static string FullToSimpleName(string fullName){
            int lastDot = fullName.LastIndexOf('.');
            return lastDot == -1 ? fullName : lastDot < fullName.Length-1 ? fullName.Substring(lastDot+1) : string.Empty;
        }

        public static List<T> ReadStructList<T>(JavaCodeParser parser, ReadStruct<T> readFunc, int defaultCapacity) where T : struct{
            var structs = new List<T>(defaultCapacity);

            parser.SkipSpacesAndSemicolons();

            while(true){
                T? structObj = readFunc();

                parser.SkipSpaces();
                
                if (structObj.HasValue)structs.Add(structObj.Value);
                else break;
            }

            return structs;
        }
    }
}
