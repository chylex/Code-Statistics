using CodeStatistics.ConsoleUtil;
using CodeStatistics.Input;
using System;
using System.Collections.Generic;

namespace CodeStatistics.Handlers.Objects{
    class AssetHandler : FileHandler.Minor{
        public enum Type{
            Image, Audio, Video, Text, Configuration, Archive
        }

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

        public override IHandlerTab[] GenerateTabs(){
            return new IHandlerTab[]{ new AssetTab() };
        }

        class AssetTab : IHandlerTab{
            public string GetName(){
                return "Assets";
            }

            public void RenderInfo(ConsoleWrapper c, int y){
                // TODO
            }
        }
    }
}
