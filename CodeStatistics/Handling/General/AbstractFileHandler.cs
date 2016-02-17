using CodeStatistics.Input;

namespace CodeStatistics.Handling.General{
    abstract class AbstractFileHandler : IFileHandler{
        public abstract int Weight { get; }

        public virtual void Process(File file, Variables.Root variables){
            variables.Increment("dirStructureFiles");
        }
    }
}
