namespace CodeStatistics.Handling{
    interface IFolderHandler : HandlerList.IWeightedEntry{
        void Process(string folder, Variables.Root variables);
    }
}
