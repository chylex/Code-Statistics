using CodeStatistics.Input;

namespace CodeStatistics.Handling.General{
    class AssetHandler : AbstractFileHandler{
        public enum Type{
            Image, Audio, Video, Document, Configuration, Archive
        }

        private readonly Type type;

        public override int Weight{
            get { return 5; }
        }

        public AssetHandler(Type type){
            this.type = type;
        }

        public override void Process(File file, Variables.Root variables){
            base.Process(file,variables);
            variables.Increment("fileTypeAssets");
        }
    }
}
