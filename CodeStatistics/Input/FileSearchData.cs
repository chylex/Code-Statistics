using System.Collections.Generic;

namespace CodeStatistics.Input{
    class FileSearchData{
        private readonly HashSet<File> files = new HashSet<File>();
        private readonly HashSet<string> folders = new HashSet<string>();

        public void Add(IOEntry entry){
            if (entry.EntryType == IOEntry.Type.File)files.Add(new File(entry.Path));
            else folders.Add(entry.Path);
        }

        public IEnumerable<File> GetFiles(){
            return files;
        }

        public IEnumerable<string> GetFolders(){
            return folders;
        }
    }
}
