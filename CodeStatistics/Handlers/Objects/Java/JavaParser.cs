using CodeStatistics.Input;
using System.Text.RegularExpressions;
using System.Linq;

namespace CodeStatistics.Handlers.Objects.Java{
    static class JavaParser{
        private static readonly Regex commentOneLine = new Regex(@"//.*",RegexOptions.Compiled);
        private static readonly Regex commentMultiLine = new Regex(@"/\*(?:.|\n)*?\*/",RegexOptions.Compiled);

        public static void Parse(string fileContents, JavaStatistics stats){
            string fileParsed = commentMultiLine.Replace(commentOneLine.Replace(fileContents,""),"");
            
            string[] linesPlain = fileContents.Split('\n').Select(line => line.TrimEnd()).Where(line => line.Length > 0).ToArray(); // lines are always \n
            string[] linesParsed = fileParsed.Split('\n').Select(line => line.TrimEnd()).Where(line => line.Length > 0).ToArray(); //  ^

            // Package
            string package = null;
            if (linesParsed.FirstOrDefault(line => line.ExtractBoth("package ",";",out package)) == null)return; // should not happen, but what if...

            stats.Packages.Add(package);

            // Lines
            int totalLines = linesPlain.Count(line => !line.TrimStart().Equals("{"));

            stats.LinesTotal += totalLines;

            // Characters
            int spaces = 0;

            int totalCharacters = linesPlain.Select(line => {
                string noSpaces = line.TrimStart(' ');
                int diff = line.Length-noSpaces.Length;

                if (diff > 0 && spaces == 0)spaces = diff;
                if (diff > 0)line = noSpaces.PadLeft(noSpaces.Length+diff/spaces,'\t'); // replaces spaces with tabs

                return line.Length;
            }).Sum();

            stats.CharactersTotal += totalCharacters;
        }
    }
}
