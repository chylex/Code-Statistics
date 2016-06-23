using CodeStatisticsCore.Input;

namespace CodeStatisticsCore.Handling{
    public interface IFileHandler{
        int Weight { get; }

        void SetupProject(Variables.Root variables);
        void Process(File file, Variables.Root variables);
        void FinalizeProject(Variables.Root variables);
    }
}
