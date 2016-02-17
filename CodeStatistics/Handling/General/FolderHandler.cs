namespace CodeStatistics.Handling.General{
    class FolderHandler : IFolderHandler{
        public int Weight{
            get { return 1; }
        }

        public void Process(string folder, Variables.Root variables){
            variables.Increment("dirStructureFolders");
        }
    }
}
