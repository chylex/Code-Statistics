using CodeStatistics.Input;

namespace CodeStatistics.Handling{
    interface IFileHandler : HandlerList.IWeightedEntry{
        void SetupProject(Variables.Root variables);
        void Process(File file, Variables.Root variables);
    }
}
