using CodeStatistics.Input;
using System.Collections.Generic;
using System.Globalization;

namespace CodeStatistics.Handling.General{
    class AssetHandler : AbstractFileHandler{
        public enum Type{
            Image, Audio, Video, Document, Configuration, Archive
        }

        private static string GetAssetTypeName(Type type){
            switch(type){
                case Type.Image: return "Images";
                case Type.Audio: return "Sounds";
                case Type.Video: return "Videos";
                case Type.Document: return "Documents";
                case Type.Configuration: return "Configuration";
                case Type.Archive: return "Archives";
                default: return "Unknown";
            }
        }

        private class AssetRowComparer : IComparer<Variables>{
            public int Compare(Variables x, Variables y){
                return int.Parse(y.GetVariable("value","0"),CultureInfo.InvariantCulture)-int.Parse(x.GetVariable("value","0"),CultureInfo.InvariantCulture);
            }
        }

        private static readonly IComparer<Variables> AssetRowSorter = new AssetRowComparer();

        private readonly Type type;

        public override int Weight{
            get { return 5; }
        }

        public AssetHandler(Type type){
            this.type = type;
        }

        public override void SetupProject(Variables.Root variables){
            base.SetupProject(variables);
            variables.AddStateObject(this,new State());
        }

        public override void Process(File file, Variables.Root variables){
            base.Process(file,variables);
            variables.Increment("fileTypeAssets");

            State state = variables.GetStateObject<State>(this);

            if (++state.Count == 1){
                variables.SetArraySorter("assetTypes",AssetRowSorter);
                state.Array = variables.AddToArray("assetTypes",new { title = GetAssetTypeName(type), value = 1 });
            }
            else{
                state.Array.UpdateVariable("value",state.Count.ToString(CultureInfo.InvariantCulture));
            }
        }

        private class State{
            public int Count;
            public Variables.ArrayAdapter Array;
        }
    }
}
