namespace CodeStatisticsCore.Handling{
    public interface IFolderHandler{
        int Weight { get; }

        void Process(string folder, Variables.Root variables);
    }
}
