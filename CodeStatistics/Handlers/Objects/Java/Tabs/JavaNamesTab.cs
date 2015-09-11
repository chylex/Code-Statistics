using CodeStatistics.ConsoleUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeStatistics.Handlers.Objects.Java.Tabs{
    class JavaNamesTab : JavaTab{
        private const int ListSize = 5;

        private List<KeyValuePair<string,string>> LongestSimpleNames = new List<KeyValuePair<string,string>>(ListSize);
        private List<KeyValuePair<string,string>> ShortestSimpleNames = new List<KeyValuePair<string,string>>(ListSize);
        private List<string> LongestFullNames = new List<string>(ListSize);
        private List<string> ShortestFullNames = new List<string>(ListSize);

        public JavaNamesTab(JavaStatistics stats) : base("Names",stats){
            IEnumerable<KeyValuePair<string,string>> convertedNames = stats.FullTypes.Select(name => JavaParseUtils.GetSimpleName(name));

            LongestSimpleNames.AddRange(convertedNames.OrderByDescending(kvp => kvp.Key.Length).Take(ListSize));
            ShortestSimpleNames.AddRange(convertedNames.OrderBy(kvp => kvp.Key.Length).Take(ListSize));

            LongestFullNames.AddRange(stats.FullTypes.OrderByDescending(name => name.Length).Take(ListSize));
            ShortestFullNames.AddRange(stats.FullTypes.OrderBy(name => name.Length).Take(ListSize));
        }

        public override void RenderInfo(ConsoleWrapper c, int y){
            int px, py;

            // Longest Simple Names
            px = c.Width/2-LongestSimpleNames.Select(kvp => kvp.Key.Length+kvp.Value.Length).Max()/2;
            c.MoveTo(px,py = y);
            c.Write("Longest Simple Names",ConsoleColor.Yellow);

            foreach(KeyValuePair<string,string> kvp in LongestSimpleNames){
                c.MoveTo(px,++py);
                c.Write(kvp.Value,ConsoleColor.DarkGray);
                c.Write(kvp.Key,ConsoleColor.White);
            }

            // Shortest Simple Names
            px = c.Width/2-ShortestSimpleNames.Select(kvp => kvp.Key.Length+kvp.Value.Length).Max()/2;
            c.MoveTo(px,py += 3);
            c.Write("Shortest Simple Names",ConsoleColor.Yellow);

            foreach(KeyValuePair<string,string> kvp in ShortestSimpleNames){
                c.MoveTo(px,++py);
                c.Write(kvp.Value,ConsoleColor.DarkGray);
                c.Write(kvp.Key,ConsoleColor.White);
            }

            // Longest Full Names
            px = c.Width/2-LongestFullNames.Select(name => name.Length).Max()/2;
            c.MoveTo(px,py += 3);
            c.Write("Longest Full Names",ConsoleColor.Yellow);

            foreach(string kvp in LongestFullNames){
                c.MoveTo(px,++py);
                c.Write(kvp,ConsoleColor.White);
            }

            // Shortest Full Names
            px = c.Width/2-ShortestFullNames.Select(name => name.Length).Max()/2;
            c.MoveTo(px,py += 3);
            c.Write("Shortest Full Names",ConsoleColor.Yellow);

            foreach(string kvp in ShortestFullNames){
                c.MoveTo(px,++py);
                c.Write(kvp,ConsoleColor.White);
            }
        }
    }
}
