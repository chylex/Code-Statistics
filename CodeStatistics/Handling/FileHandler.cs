using CodeStatistics.Input;

namespace CodeStatistics.Handling{
    abstract class FileHandler{
        public abstract int Weight { get; }

        public bool IsFileValid(File file){
            return true;
        }

        public abstract void Process(File file, Variables.Root variables);
    }
}
