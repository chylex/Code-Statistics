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
            IEnumerable<KeyValuePair<string,string>> convertedNames = stats.FullTypes.Select(name => GetSimpleName(name));

            LongestSimpleNames.AddRange(convertedNames.OrderByDescending(kvp => kvp.Key.Length).Take(ListSize));
            ShortestSimpleNames.AddRange(convertedNames.OrderBy(kvp => kvp.Key.Length).Take(ListSize));

            LongestFullNames.AddRange(stats.FullTypes.OrderByDescending(name => name.Length).Take(ListSize));
            ShortestFullNames.AddRange(stats.FullTypes.OrderBy(name => name.Length).Take(ListSize));
        }

        public override void RenderInfo(ConsoleWrapper c, int y){
            int px, py;

            px = c.Width/2-LongestSimpleNames.Select(kvp => kvp.Key.Length+kvp.Value.Length).Max()/2;
            c.MoveTo(px,py = y);
            c.SetForeground(ConsoleColor.Yellow);
            c.Write("Longest Simple Names");

            for(int index = 0; index < LongestSimpleNames.Count; index++){
                c.MoveTo(px,++py);
                c.SetForeground(ConsoleColor.DarkGray);
                c.Write(LongestSimpleNames[index].Value);
                c.SetForeground(ConsoleColor.White);
                c.Write(LongestSimpleNames[index].Key);
            }

            px = c.Width/2-ShortestSimpleNames.Select(kvp => kvp.Key.Length+kvp.Value.Length).Max()/2;
            c.MoveTo(px,py += 3);
            c.SetForeground(ConsoleColor.Yellow);
            c.Write("Shortest Simple Names");

            for(int index = 0; index < ShortestSimpleNames.Count; index++){
                c.MoveTo(px,++py);
                c.SetForeground(ConsoleColor.DarkGray);
                c.Write(ShortestSimpleNames[index].Value);
                c.SetForeground(ConsoleColor.White);
                c.Write(ShortestSimpleNames[index].Key);
            }

            px = c.Width/2-LongestFullNames.Select(name => name.Length).Max()/2;
            c.MoveTo(px,py += 3);
            c.SetForeground(ConsoleColor.Yellow);
            c.Write("Longest Full Names");

            for(int index = 0; index < LongestFullNames.Count; index++){
                c.MoveTo(px,++py);
                c.SetForeground(ConsoleColor.White);
                c.Write(LongestFullNames[index]);
            }

            px = c.Width/2-ShortestFullNames.Select(name => name.Length).Max()/2;
            c.MoveTo(px,py += 3);
            c.SetForeground(ConsoleColor.Yellow);
            c.Write("Shortest Full Names");

            for(int index = 0; index < ShortestFullNames.Count; index++){
                c.MoveTo(px,++py);
                c.SetForeground(ConsoleColor.White);
                c.Write(ShortestFullNames[index]);
            }
        }

        private KeyValuePair<string,string> GetSimpleName(string fullName){
            int index = fullName.LastIndexOf('.');
            return new KeyValuePair<string,string>(index == -1 ? fullName : fullName.Substring(index+1),index == -1 ? "" : fullName.Substring(0,index+1));
        }
    }
}
