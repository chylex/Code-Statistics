using CodeStatistics.Input;

namespace CodeStatistics.Handling.General{
    class AssetHandler : IFileHandler{
        public int Weight{
            get { return 5; }
        }

        public bool IsFileValid(File file){
            return true;
        }

        public void Process(File file, Variables.Root variables){
            
        }
    }
}
