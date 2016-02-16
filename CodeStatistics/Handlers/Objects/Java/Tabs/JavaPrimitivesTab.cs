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
    }
}
