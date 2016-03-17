using CodeStatistics.Collections;
using CodeStatistics.Input;
using System;
using System.Collections.Generic;

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

        private static readonly Comparison<Variables> AssetRowSorter = (x, y) => y.GetVariable("value",0)-x.GetVariable("value",0);
        private static readonly Comparison<Variables> AssetSizeRowSorter = (x, y) => Math.Sign((long)y.GetVariable("comph",0)<<32|y.GetVariable("compl",0)-((long)x.GetVariable("comph",0)<<32|x.GetVariable("compl",0)));

        private readonly Type type;

        public override int Weight{
            get { return 5; }
        }

        public AssetHandler(Type type){
            this.type = type;
        }

        public override void SetupProject(Variables.Root variables){
            base.SetupProject(variables);
            variables.AddFlag("assets");
            variables.AddStateObject(this,new State());
        }

        public override void Process(File file, Variables.Root variables){
            base.Process(file,variables);
            variables.Increment("fileTypeAssets");

            State state = variables.GetStateObject<State>(this);

            if (++state.Count == 1){
                variables.SetArraySorter("assetTypes",AssetRowSorter);
                state.TypeEntry = variables.AddToArray("assetTypes",new { title = GetAssetTypeName(type), value = 0 });

                variables.SetArraySorter("assetSizes",AssetSizeRowSorter);
                state.SizeEntry = variables.AddToArray("assetSizes",new { title = GetAssetTypeName(type), compl = 0, comph = 0, size = 0, units = "" });
            }

            state.Size += file.SizeInBytes;
        }

        public override void FinalizeProject(Variables.Root variables){
            base.FinalizeProject(variables);

            State state = variables.GetStateObject<State>(this);
            state.TypeEntry.UpdateVariable("value",state.Count);

            KeyValuePair<long,string> size = IOUtils.GetFriendlyFileSize(state.Size);
            state.SizeEntry.UpdateVariable("size",(int)size.Key);
            state.SizeEntry.UpdateVariable("units",size.Value);

            state.SizeEntry.UpdateVariable("compl",(int)(state.Size&((1L<<32)-1L)));
            state.SizeEntry.UpdateVariable("comph",(int)(state.Size>>32));
        }

        private class State{
            public int Count;
            public long Size;
            public Variables.ArrayAdapter TypeEntry;
            public Variables.ArrayAdapter SizeEntry;
        }
    }
}
