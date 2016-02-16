using System.Collections.Generic;
using System.Linq;

namespace CodeStatistics.Handlers.Objects.Java.Tabs{
    class JavaFilesTab{
        const int fileCount = 10;

        private readonly List<KeyValuePair<string,int>> mostLines = new List<KeyValuePair<string,int>>(fileCount);
        private readonly List<KeyValuePair<string,int>> leastLines = new List<KeyValuePair<string,int>>(fileCount);
        private readonly int totalLines, averageLines;

        private readonly List<KeyValuePair<string,long>> mostCharacters = new List<KeyValuePair<string,long>>(fileCount);
        private readonly List<KeyValuePair<string,long>> leastCharacters = new List<KeyValuePair<string,long>>(fileCount);
        private readonly long totalCharacters, averageCharacters;
        
        private readonly List<KeyValuePair<string,int>> mostImports = new List<KeyValuePair<string,int>>(fileCount);
        private readonly List<KeyValuePair<string,int>> leastImports = new List<KeyValuePair<string,int>>(fileCount);
        private readonly int totalImports, averageImports;

        public JavaFilesTab(JavaStatistics stats){
            Dictionary<string,JavaStatistics.JavaFileInfo> dict = stats.FileInfo;

            totalLines = stats.LinesTotal;
            averageLines = totalLines/dict.Count;
            mostLines.AddRange(dict.OrderByDescending(kvp => kvp.Value.Lines).Take(fileCount).Select(kvp => new KeyValuePair<string,int>(JavaParseUtils.GetSimpleName(kvp.Key).Key,kvp.Value.Lines)));
            leastLines.AddRange(dict.OrderBy(kvp => kvp.Value.Lines).Take(fileCount).Select(kvp => new KeyValuePair<string,int>(JavaParseUtils.GetSimpleName(kvp.Key).Key+".java",kvp.Value.Lines)));

            totalCharacters = stats.CharactersTotal;
            averageCharacters = totalCharacters/dict.Count;
            mostCharacters.AddRange(dict.OrderByDescending(kvp => kvp.Value.Characters).Take(fileCount).Select(kvp => new KeyValuePair<string,long>(JavaParseUtils.GetSimpleName(kvp.Key).Key+".java",kvp.Value.Characters)));
            leastCharacters.AddRange(dict.OrderBy(kvp => kvp.Value.Characters).Take(fileCount).Select(kvp => new KeyValuePair<string,long>(JavaParseUtils.GetSimpleName(kvp.Key).Key+".java",kvp.Value.Characters)));

            totalImports = stats.ImportsTotal;
            averageImports = totalImports/dict.Count;
            mostImports.AddRange(dict.OrderByDescending(kvp => kvp.Value.Imports).Take(fileCount).Select(kvp => new KeyValuePair<string,int>(JavaParseUtils.GetSimpleName(kvp.Key).Key+".java",kvp.Value.Imports)));
            leastImports.AddRange(dict.OrderBy(kvp => kvp.Value.Imports).Take(fileCount).Select(kvp => new KeyValuePair<string,int>(JavaParseUtils.GetSimpleName(kvp.Key).Key+".java",kvp.Value.Imports)));
        }
    }
}
