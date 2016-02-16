using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeStatistics.Handlers.Objects.Assets{
    class AssetTab : IHandlerTab{
        private static readonly AssetHandler.Type[] types = Enum.GetValues(typeof(AssetHandler.Type)).Cast<AssetHandler.Type>().ToArray();

        private readonly int totalAssets;
        private readonly Dictionary<AssetHandler.Type,AssetTypeInfo> info = new Dictionary<AssetHandler.Type,AssetTypeInfo>();

        public AssetTab(AssetHandler handler){
            totalAssets = handler.GetAssetFileCount();

            foreach(AssetHandler.Type type in types){
                List<string> exts = handler.GetAssetExtensions(type);
                info[type] = new AssetTypeInfo(exts.Select(ext => new KeyValuePair<string,int>(ext,handler.GetExtCount(ext))).Where(kvp => kvp.Value != 0));
            }
        }

        public string GetName(){
            return "Assets";
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
