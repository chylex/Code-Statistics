using CodeStatistics.ConsoleUtil;
using CodeStatistics.Handlers.Objects.Java.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeStatistics.Handlers.Objects.Java.Tabs{
    class JavaPrimitivesTab : JavaTab{
        private readonly Dictionary<string,KeyValuePair<int,decimal>> info = new Dictionary<string,KeyValuePair<int,decimal>>();

        public JavaPrimitivesTab(JavaStatistics stats) : base("Primitives",stats){
            int total = stats.PrimitiveCounts.Values.Sum();

            foreach(KeyValuePair<JavaPrimitives,int> count in stats.PrimitiveCounts.OrderByDescending(kvp => kvp.Value)){
                info.Add(JavaPrimitivesFunc.ToString(count.Key),new KeyValuePair<int,decimal>(count.Value,Math.Round(100*(decimal)count.Value/total,2)));
            }
        }

        public override void RenderInfo(ConsoleWrapper c, int y){
            int py = y;

            foreach(KeyValuePair<string,KeyValuePair<int,decimal>> kvp in info){
                c.MoveTo(c.Width/2-kvp.Key.Length-1,py++);
                c.SetForeground(ConsoleColor.White);
                c.Write("{0}: ",kvp.Key);
                c.SetForeground(ConsoleColor.Gray);
                c.Write("{0} ({1}%)",kvp.Value.Key,kvp.Value.Value);
            }
        }
    }
}
