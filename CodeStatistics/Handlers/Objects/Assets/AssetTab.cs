using CodeStatistics.ConsoleUtil;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CodeStatistics.Handlers.Objects.Assets{
    class AssetTab : IHandlerTab{
        private static readonly AssetHandler.Type[] types = Enum.GetValues(typeof(AssetHandler.Type)).Cast<AssetHandler.Type>().ToArray();

        private readonly int totalAssets;
        private readonly Dictionary<AssetHandler.Type,AssetTypeInfo> info = new Dictionary<AssetHandler.Type,AssetTypeInfo>();

        public AssetTab(AssetHandler handler){
            totalAssets = handler.GetAssetCount();

            foreach(AssetHandler.Type type in types){
                List<string> exts = handler.GetAssetExtensions(type);
                info[type] = new AssetTypeInfo(exts.Select(ext => new KeyValuePair<string,int>(ext,handler.GetExtCount(ext))).Where(kvp => kvp.Value != 0));
            }
        }

        public string GetName(){
            return "Assets";
        }

        public void RenderInfo(ConsoleWrapper c, int y){
            c.SetForeground(ConsoleColor.White);
            c.WriteCenter(y,"Total Assets: ");
            c.Write(totalAssets.ToString(),ConsoleColor.Gray);
            
            const int dist = 28;

            int py = y+3;
            int[] columnWidths = new int[]{ 0, 0, 0 };

            for(int ind = 0, col = 0, width = 0; ind < types.Length; ind++, col = ind/2){
                AssetTypeInfo typeInfo = info[types[ind]];

                width = AssetHandler.TypeNames[ind].Length+2+typeInfo.Total.ToString().Length;
                if (width > columnWidths[col])columnWidths[col] = width;

                foreach(KeyValuePair<string,int> kvp in typeInfo.ExtCount){
                    width = kvp.Key.Length+(typeInfo.MaxLength-kvp.Key.Length)+kvp.Value.ToString().Length+Math.Round(100*(decimal)kvp.Value/typeInfo.Total,2).ToString().Length+6;
                    if (width > columnWidths[col])columnWidths[col] = width;
                }
            }

            for(int ind = 0, col = 0; ind < types.Length; ind++, col = ind/2){
                int px = c.Width/2+(col-1)*dist-(columnWidths[col]/2);

                AssetTypeInfo typeInfo = info[types[ind]];

                c.Write(px,py,AssetHandler.TypeNames[ind]+": ",ConsoleColor.Yellow);
                c.Write(typeInfo.Total.ToString(),ConsoleColor.Gray);

                foreach(KeyValuePair<string,int> kvp in typeInfo.ExtCount.OrderByDescending(kvp => kvp.Value)){
                    c.MoveTo(px,++py);
                    c.SetForeground(ConsoleColor.White);
                    c.Write(".{0}: ",kvp.Key);
                    c.SetForeground(ConsoleColor.Gray);
                    c.Write(new string(' ',typeInfo.MaxLength-kvp.Key.Length));
                    c.Write("{0} ({1}%)",kvp.Value,Math.Round(100*(decimal)kvp.Value/typeInfo.Total,2));
                }

                c.MoveTo(px,py += 3);

                if (ind%2 != 0){
                    px += dist;
                    py = y+3;
                }
            }
        }

        struct AssetTypeInfo{
            public readonly IEnumerable<KeyValuePair<string,int>> ExtCount;
            public readonly int Total;
            public readonly int MaxLength;

            public AssetTypeInfo(IEnumerable<KeyValuePair<string,int>> extCount){
                this.ExtCount = extCount;
                this.Total = extCount.Select(kvp => kvp.Value).DefaultIfEmpty(0).Sum();
                this.MaxLength = extCount.Select(kvp => kvp.Key.Length).DefaultIfEmpty(0).Max();
            }
        }
    }
}
