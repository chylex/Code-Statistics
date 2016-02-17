using CodeStatistics.Input;

namespace CodeStatistics.Handling.General{
    abstract class AbstractFileHandler : IFileHandler{
        public abstract int Weight { get; }

        public virtual void SetupProject(Variables.Root variables){}

        public virtual void Process(File file, Variables.Root variables){
            variables.Increment("dirStructureFiles");
        }
    }
}
