using CodeStatistics.Input;
using System.Text.RegularExpressions;

namespace CodeStatistics.Handlers.Objects.Java{
    static class JavaParser{
        private static readonly Regex commentOneLine = new Regex(@"//.*",RegexOptions.Compiled);
        private static readonly Regex commentMultiLine = new Regex(@"/\*(?:.|\n)*?\*/",RegexOptions.Compiled);

        public static void Parse(File file, JavaStatistics stats){
            string filePlain = file.Read();
            string fileParsed = commentMultiLine.Replace(commentOneLine.Replace(filePlain,""),"");
            
            string[] linesPlain = filePlain.Split('\n'); // lines are always \n
            string[] linesParsed = fileParsed.Split('\n'); // ^


        }
    }
}
