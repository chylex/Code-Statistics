using CodeStatistics.Handlers.Objects.Assets;
using CodeStatistics.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeStatistics.Handlers.Objects{
    class AssetHandler : FileHandler.Minor{
        public enum Type{
            Image, Text, Video, Configuration, Audio, Archive
        }

        public static readonly string[] TypeNames = new string[]{ "Images", "Text", "Video", "Configuration", "Audio", "Archives" };

        private readonly Dictionary<Type,List<string>> types = new Dictionary<Type,List<string>>();
        private readonly Dictionary<string,int> count = new Dictionary<string,int>();

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

        public List<string> GetAssetExtensions(Type type){
            return types[type];
        }

        public int GetAssetFileCount(){
            return count.Values.Sum();
        }

        public int GetExtCount(string ext){
            int amount;
            return count.TryGetValue(ext,out amount) ? amount : 0;
        }

        public override IHandlerTab[] GenerateTabs(){
            return new IHandlerTab[]{ new AssetTab(this) };
        }

        public override int GetWeight(){
            return 1;
        }
    }
}
