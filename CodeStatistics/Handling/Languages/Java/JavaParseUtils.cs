using System.Text.RegularExpressions;

namespace CodeStatistics.Handling.Languages.Java{
    public static class JavaParseUtils{
        private static readonly Regex RegexString = new Regex(@"([""'])(?:\\[\\'""btnfru0-7]|[^\\""])*?\1",RegexOptions.Compiled); // verbatim strings with quotes need "" for literal
        private static readonly Regex RegexCommentSingle = new Regex(@"//.*?$",RegexOptions.Compiled | RegexOptions.Multiline);
        private static readonly Regex RegexCommentMulti = new Regex(@"/\*.*?\*/",RegexOptions.Compiled | RegexOptions.Singleline);
        
        public static string ProcessCodeFile(string code){
            string processed = code;
            processed = RegexString.Replace(processed,@""""""); // beautiful
            processed = RegexCommentSingle.Replace(processed,"");
            processed = RegexCommentMulti.Replace(processed,"");
            return processed;
        }
    }
}
