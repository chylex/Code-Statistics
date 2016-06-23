using CodeStatisticsCore.Input;

namespace CodeStatisticsCore.Handling.Files{
    public abstract class AbstractFileHandler : IFileHandler{
        public abstract int Weight { get; }

        public virtual void SetupProject(Variables.Root variables){}

        public virtual void Process(File file, Variables.Root variables){
            variables.Increment("dirStructureFiles");
        }

        public virtual void FinalizeProject(Variables.Root variables){}
    }
}
