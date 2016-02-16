namespace CodeStatistics.Input{
    struct IOEntry{
        public enum Type{
            File, Folder
        }

        public readonly Type EntryType;
        public readonly string Path;

        public IOEntry(Type type, string path){
            this.EntryType = type;
            this.Path = path;
        }
    }
}