using CodeStatistics.ConsoleUtil;
using CodeStatistics.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeStatistics.Handlers.Objects{
    class AssetHandler : FileHandler.Minor{
        public enum Type{
            Image, Text, Video, Configuration, Audio, Archive
        }

        private static string[] typeNames = new string[]{ "Images", "Text", "Video", "Configuration", "Audio", "Archives" };

        private Dictionary<Type,List<string>> types = new Dictionary<Type,List<string>>();
        private Dictionary<string,int> count = new Dictionary<string,int>();

        public AssetHandler(int priority) : base(priority){
            foreach(Type type in Enum.GetValues(typeof(Type)))types[type] = new List<string>();
        }

        public AssetHandler SetType(string extension, Type type){
            types[type].Add(extension);
            return this;
        }

        public override void Handle(File file){
            int amount;
            count[file.Ext] = count.TryGetValue(file.Ext,out amount) ? ++amount : 1;
        }

        private int GetExtCount(string ext){
            int amount;
            return count.TryGetValue(ext,out amount) ? amount : 0;
        }

        public override IHandlerTab[] GenerateTabs(){
            return new IHandlerTab[]{ new AssetTab(this) };
        }

        class AssetTab : IHandlerTab{
            private AssetHandler handler;

            public AssetTab(AssetHandler handler){
                this.handler = handler;
            }

            public string GetName(){
                return "Assets";
            }

            public void RenderInfo(ConsoleWrapper c, int y){
                c.SetForeground(ConsoleColor.White);
                c.WriteCenter(y,"Total Assets: ");
                c.SetForeground(ConsoleColor.Gray);
                c.Write(handler.count.Values.Sum().ToString());
                
                Array types = Enum.GetValues(typeof(Type));
                const int dist = 28;

                int px = c.Width/2-dist-5, py = y+3;

                for(int ind = 0; ind < types.Length; ind++){
                    Type currentType = (Type)types.GetValue(ind);
                    List<string> exts = handler.types[currentType];

                    IEnumerable<KeyValuePair<string,int>> usedExts = exts.Select(ext => new KeyValuePair<string,int>(ext,handler.GetExtCount(ext))).Where(kvp => kvp.Value != 0);

                    int total = usedExts.Select(kvp => kvp.Value).DefaultIfEmpty(0).Sum();
                    int longestExt = usedExts.Select(kvp => kvp.Key.Length).DefaultIfEmpty(0).Max();

                    c.SetForeground(ConsoleColor.Yellow);
                    c.Write(px,py,typeNames[ind]+": ");
                    c.SetForeground(ConsoleColor.Gray);
                    c.Write(total.ToString());

                    foreach(KeyValuePair<string,int> kvp in usedExts.OrderByDescending(kvp => kvp.Value).ToList()){
                        c.MoveTo(px,++py);
                        c.SetForeground(ConsoleColor.White);
                        c.Write(".");
                        c.Write(kvp.Key);
                        c.Write(": ");
                        c.SetForeground(ConsoleColor.Gray);
                        c.Write(new string(' ',longestExt-kvp.Key.Length));
                        c.Write(kvp.Value.ToString());
                        c.Write(" (");
                        c.Write(Math.Round(100*(decimal)kvp.Value/total,2).ToString());
                        c.Write("%)");
                    }

                    c.MoveTo(px,py += 3);

                    if (ind%2 != 0){
                        px += dist;
                        py = y+3;
                    }
                }
            }
        }
    }
}
