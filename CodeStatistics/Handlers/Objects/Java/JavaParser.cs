using CodeStatistics.Input;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace CodeStatistics.Handlers.Objects.Java{
    static class JavaParser{
        public static void Parse(string fileContents, JavaStatistics stats){
            string fileParsed = JavaParseUtils.commentMultiLine.Replace(JavaParseUtils.commentOneLine.Replace(fileContents,""),"");
            
            string[] linesPlain = fileContents.Split('\n').Select(line => line.TrimEnd()).Where(line => line.Length > 0).ToArray(); // lines are always \n
            string[] linesParsed = fileParsed.Split('\n').Select(line => line.TrimEnd()).Where(line => line.Length > 0).ToArray(); //  ^

            // Final variables
            string _package = null;
            JavaType _currentType = JavaType.Invalid;
            string _simpleType = null;
            string _fullType;
            int _totalLines;
            int _totalCharacters;

            // Package
            if (linesParsed.FirstOrDefault(line => line.ExtractBoth("package ",";",out _package)) == null)return; // should not happen, but what if...

            stats.Packages.Add(_package);

            // Current type
            string typeLine = linesParsed.FirstOrDefault(line => JavaParseUtils.typeIdentifiers.Any(identifier => line.Contains(identifier)));
            if (typeLine == null)return; // should not happen either, but we know the drill...

            KeyValuePair<JavaType,string> typeData = JavaParseUtils.GetType(typeLine);
            _currentType = typeData.Key;
            _simpleType = typeData.Value;
            _fullType = _package+"."+_simpleType;

            // Lines
            _totalLines = linesPlain.Count(line => !line.TrimStart().Equals("{"));



            stats.LinesTotal += _totalLines;

            // Characters
            int spaces = 0;

            _totalCharacters = linesPlain.Select(line => {
                string noSpaces = line.TrimStart(' ');

                int diff = line.Length-noSpaces.Length;
                if (diff > 0 && spaces == 0)spaces = diff;

                return diff > 0 ? noSpaces.PadLeft(noSpaces.Length+diff/spaces,'\t').Length : line.Length; // replaces spaces with tabs if there are any
            }).Sum();



            stats.CharactersTotal += _totalCharacters;
        }
    }
}
