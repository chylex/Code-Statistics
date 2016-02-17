using CodeStatistics.Input;

namespace CodeStatistics.Handling.General{
    class UnknownHandler : IFileHandler{
        public int Weight{
            get { return 1; }
        }

        public bool IsFileValid(File file){
            return true;
        }

        public void Process(File file, Variables.Root variables){
            
        }
    }
}
