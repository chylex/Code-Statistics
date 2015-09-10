using CodeStatistics.Collections;
using CodeStatistics.ConsoleUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeStatistics.Handlers.Objects.Java.Tabs{
    class JavaSyntaxTab : JavaTab{
        private readonly int totalCycles, cycleWidth;
        private readonly List<Triple<string,int,decimal>> dataCycles = new List<Triple<string,int,decimal>>();

        public JavaSyntaxTab(JavaStatistics stats) : base("Syntax",stats){
            totalCycles = stats.SyntaxFor+stats.SyntaxForEach+stats.SyntaxWhile+stats.SyntaxDoWhile;
            dataCycles.Add(new Triple<string,int,decimal>("for",stats.SyntaxFor,Math.Round(100*(decimal)stats.SyntaxFor/totalCycles)));
            dataCycles.Add(new Triple<string,int,decimal>("enhanced for",stats.SyntaxForEach,Math.Round(100*(decimal)stats.SyntaxForEach/totalCycles)));
            dataCycles.Add(new Triple<string,int,decimal>("while",stats.SyntaxWhile,Math.Round(100*(decimal)stats.SyntaxWhile/totalCycles)));
            dataCycles.Add(new Triple<string,int,decimal>("do while",stats.SyntaxDoWhile,Math.Round(100*(decimal)stats.SyntaxDoWhile/totalCycles)));
            cycleWidth = Math.Max(6+dataCycles.Max(kvp => kvp.First.Length+kvp.Second.ToString().Length+kvp.Third.ToString().Length),"Total Cycles: ".Length+totalCycles.ToString().Length);
        }

        public override void RenderInfo(ConsoleWrapper c, int y){
            int px, py;

            // Fields

            // Methods

            // Cycles
            px = c.Width-cycleWidth-8;
            c.MoveTo(px,py = y);
            c.Write("Total Cycles: ",ConsoleColor.Yellow);
            c.Write(totalCycles.ToString(),ConsoleColor.Gray);

            foreach(Triple<string,int,decimal> data in dataCycles){
                c.MoveTo(px,++py);
                c.Write(data.First+": ",ConsoleColor.White);
                c.Write("{0} ({1}%)",data.Second,data.Third);
            }

            // Misc
        }
    }
}
