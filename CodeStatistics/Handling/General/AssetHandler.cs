using CodeStatistics.Input;

namespace CodeStatistics.Handling.General{
    class AssetHandler : IFileHandler{
        public enum Type{
            Image, Audio, Video, Document, Configuration, Archive
        }

        private readonly Type type;

        public int Weight{
            get { return 5; }
        }

        public AssetHandler(Type type){
            this.type = type;
        }

        public void Process(File file, Variables.Root variables){
            
        }
    }
}
