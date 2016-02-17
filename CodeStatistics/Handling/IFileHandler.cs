using CodeStatistics.Input;

namespace CodeStatistics.Handling{
    interface IFileHandler : HandlerList.IWeightedEntry{
        void Process(File file, Variables.Root variables);
    }
}
