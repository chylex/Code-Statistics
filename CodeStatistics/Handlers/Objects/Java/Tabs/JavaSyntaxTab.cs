using CodeStatistics.Collections;
using CodeStatistics.ConsoleUtil;
using CodeStatistics.Handlers.Objects.Java.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeStatistics.Handlers.Objects.Java.Tabs{
    class JavaSyntaxTab : JavaTab{
        private readonly int totalCycles, cycleWidth;
        private readonly List<Triple<string,int,decimal>> dataCycles = new List<Triple<string,int,decimal>>();

        private readonly Dictionary<JavaModifiers.Visibility,KeyValuePair<int,decimal>> fieldVisibility = new Dictionary<JavaModifiers.Visibility,KeyValuePair<int,decimal>>();
        private readonly Dictionary<JavaModifiers.Scope,KeyValuePair<int,decimal>> fieldScope = new Dictionary<JavaModifiers.Scope,KeyValuePair<int,decimal>>();
        private readonly Dictionary<JavaModifiers.Finality,KeyValuePair<int,decimal>> fieldFinality = new Dictionary<JavaModifiers.Finality,KeyValuePair<int,decimal>>();

        private readonly Dictionary<JavaModifiers.Visibility,KeyValuePair<int,decimal>> methodVisibility = new Dictionary<JavaModifiers.Visibility,KeyValuePair<int,decimal>>();
        private readonly Dictionary<JavaModifiers.Scope,KeyValuePair<int,decimal>> methodScope = new Dictionary<JavaModifiers.Scope,KeyValuePair<int,decimal>>();
        private readonly Dictionary<JavaModifiers.Finality,KeyValuePair<int,decimal>> methodFinality = new Dictionary<JavaModifiers.Finality,KeyValuePair<int,decimal>>();

        public JavaSyntaxTab(JavaStatistics stats) : base("Syntax",stats){
            totalCycles = stats.SyntaxFor+stats.SyntaxForEach+stats.SyntaxWhile+stats.SyntaxDoWhile;
            dataCycles.Add(new Triple<string,int,decimal>("for",stats.SyntaxFor,Math.Round(100*(decimal)stats.SyntaxFor/totalCycles)));
            dataCycles.Add(new Triple<string,int,decimal>("enh for",stats.SyntaxForEach,Math.Round(100*(decimal)stats.SyntaxForEach/totalCycles)));
            dataCycles.Add(new Triple<string,int,decimal>("while",stats.SyntaxWhile,Math.Round(100*(decimal)stats.SyntaxWhile/totalCycles)));
            dataCycles.Add(new Triple<string,int,decimal>("do while",stats.SyntaxDoWhile,Math.Round(100*(decimal)stats.SyntaxDoWhile/totalCycles)));
            cycleWidth = Math.Max(6+dataCycles.Max(kvp => kvp.First.Length+kvp.Second.ToString().Length+kvp.Third.ToString().Length),"Total Cycles: ".Length+totalCycles.ToString().Length);

            foreach(JavaModifiers.Visibility visibility in Enum.GetValues(typeof(JavaModifiers.Visibility))){
                fieldVisibility[visibility] = new KeyValuePair<int,decimal>(stats.FieldVisibility[visibility],Math.Round(100*(decimal)stats.FieldVisibility[visibility]/stats.FieldVisibility.Values.Sum()));
                methodVisibility[visibility] = new KeyValuePair<int,decimal>(stats.MethodVisibility[visibility],Math.Round(100*(decimal)stats.MethodVisibility[visibility]/stats.MethodVisibility.Values.Sum()));
            }

            foreach(JavaModifiers.Scope scope in Enum.GetValues(typeof(JavaModifiers.Scope))){
                fieldScope[scope] = new KeyValuePair<int,decimal>(stats.FieldScope[scope],Math.Round(100*(decimal)stats.FieldScope[scope]/stats.FieldScope.Values.Sum()));
                methodScope[scope] = new KeyValuePair<int,decimal>(stats.MethodScope[scope],Math.Round(100*(decimal)stats.MethodScope[scope]/stats.MethodScope.Values.Sum()));
            }

            foreach(JavaModifiers.Finality finality in Enum.GetValues(typeof(JavaModifiers.Finality))){
                if (finality != JavaModifiers.Finality.Abstract){
                    fieldFinality[finality] = new KeyValuePair<int,decimal>(stats.FieldFinality[finality],Math.Round(100*(decimal)stats.FieldFinality[finality]/stats.FieldFinality.Values.Sum()));
                }

                methodFinality[finality] = new KeyValuePair<int,decimal>(stats.MethodFinality[finality],Math.Round(100*(decimal)stats.MethodFinality[finality]/stats.MethodFinality.Values.Sum()));
            }
        }

        public override void RenderInfo(ConsoleWrapper c, int y){
            int px, py;

            // Fields
            px = 8;
            c.MoveTo(px,py = y);
            c.Write("Total Fields: ",ConsoleColor.Yellow);
            c.Write(stats.FieldsTotal.ToString(),ConsoleColor.Gray);

            c.MoveTo(px,py += 2);
            c.Write("Visibility",ConsoleColor.Yellow);

            foreach(KeyValuePair<JavaModifiers.Visibility,KeyValuePair<int,decimal>> kvp in fieldVisibility.OrderByDescending(kvp => kvp.Value.Key)){
                c.MoveTo(px,++py);
                c.SetForeground(ConsoleColor.White);
                c.Write("{0}: ",kvp.Key.GetName());
                c.SetForeground(ConsoleColor.Gray);
                c.Write("{0} ({1}%)",kvp.Value.Key,kvp.Value.Value);
            }

            c.MoveTo(px,py += 2);
            c.Write("Scope",ConsoleColor.Yellow);

            foreach(KeyValuePair<JavaModifiers.Scope,KeyValuePair<int,decimal>> kvp in fieldScope.OrderByDescending(kvp => kvp.Value.Key)){
                c.MoveTo(px,++py);
                c.SetForeground(ConsoleColor.White);
                c.Write("{0}: ",kvp.Key.GetName());
                c.SetForeground(ConsoleColor.Gray);
                c.Write("{0} ({1}%)",kvp.Value.Key,kvp.Value.Value);
            }

            c.MoveTo(px,py += 2);
            c.Write("Finality",ConsoleColor.Yellow);

            foreach(KeyValuePair<JavaModifiers.Finality,KeyValuePair<int,decimal>> kvp in fieldFinality.OrderByDescending(kvp => kvp.Value.Key)){
                c.MoveTo(px,++py);
                c.SetForeground(ConsoleColor.White);
                c.Write("{0}: ",kvp.Key.GetName());
                c.SetForeground(ConsoleColor.Gray);
                c.Write("{0} ({1}%)",kvp.Value.Key,kvp.Value.Value);
            }

            // Methods
            px = c.Width/2-("Total Methods: ".Length+stats.MethodsTotal.ToString().Length+1)/2; // +1 adjustment
            c.MoveTo(px,py = y);
            c.Write("Total Methods: ",ConsoleColor.Yellow);
            c.Write(stats.MethodsTotal.ToString(),ConsoleColor.Gray);

            c.MoveTo(px,py += 2);
            c.Write("Visibility",ConsoleColor.Yellow);

            foreach(KeyValuePair<JavaModifiers.Visibility,KeyValuePair<int,decimal>> kvp in methodVisibility.OrderByDescending(kvp => kvp.Value.Key)){
                c.MoveTo(px,++py);
                c.SetForeground(ConsoleColor.White);
                c.Write("{0}: ",kvp.Key.GetName());
                c.SetForeground(ConsoleColor.Gray);
                c.Write("{0} ({1}%)",kvp.Value.Key,kvp.Value.Value);
            }

            c.MoveTo(px,py += 2);
            c.Write("Scope",ConsoleColor.Yellow);

            foreach(KeyValuePair<JavaModifiers.Scope,KeyValuePair<int,decimal>> kvp in methodScope.OrderByDescending(kvp => kvp.Value.Key)){
                c.MoveTo(px,++py);
                c.SetForeground(ConsoleColor.White);
                c.Write("{0}: ",kvp.Key.GetName());
                c.SetForeground(ConsoleColor.Gray);
                c.Write("{0} ({1}%)",kvp.Value.Key,kvp.Value.Value);
            }

            c.MoveTo(px,py += 2);
            c.Write("Finality",ConsoleColor.Yellow);

            foreach(KeyValuePair<JavaModifiers.Finality,KeyValuePair<int,decimal>> kvp in methodFinality.OrderByDescending(kvp => kvp.Value.Key)){
                c.MoveTo(px,++py);
                c.SetForeground(ConsoleColor.White);
                c.Write("{0}: ",kvp.Key.GetName());
                c.SetForeground(ConsoleColor.Gray);
                c.Write("{0} ({1}%)",kvp.Value.Key,kvp.Value.Value);
            }

            // Cycles & Misc
            px = c.Width-Math.Max(cycleWidth,"Total Switch Blocks: ".Length+stats.SyntaxSwitches.ToString().Length)-8;
            c.MoveTo(px,py = y);
            c.Write("Total Cycles: ",ConsoleColor.Yellow);
            c.Write(totalCycles.ToString(),ConsoleColor.Gray);

            foreach(Triple<string,int,decimal> data in dataCycles){
                c.MoveTo(px,++py);
                c.Write(data.First+": ",ConsoleColor.White);
                c.Write("{0} ({1}%)",data.Second,data.Third);
            }

            c.MoveTo(px,py = py+2);
            c.Write("Total Switch Blocks: ",ConsoleColor.Yellow);
            c.Write(stats.SyntaxSwitches.ToString(),ConsoleColor.Gray);

            c.MoveTo(px,py = py+2);
            c.Write("Total Try Blocks: ",ConsoleColor.Yellow);
            c.Write(stats.SyntaxTry.ToString(),ConsoleColor.Gray);
        }
    }
}
