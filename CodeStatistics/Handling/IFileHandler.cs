using CodeStatistics.Input;

namespace CodeStatistics.Handling{
    interface IFileHandler : HandlerList.IWeightedEntry{
        bool IsFileValid(File file);
        void Process(File file, Variables.Root variables);
    }
}
