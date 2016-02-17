using System.Collections.Generic;

namespace CodeStatistics.Input{
    class FileSearchData{
        private readonly HashSet<File> files = new HashSet<File>();
        private readonly HashSet<string> folders = new HashSet<string>();

        public IEnumerable<File> Files{
            get { return files; }
        }

        public IEnumerable<string> Folders{
            get { return folders; }
        }

        public int EntryCount{
            get { return files.Count+folders.Count; }
        }

        public void Add(IOEntry entry){
            if (entry.EntryType == IOEntry.Type.File)files.Add(new File(entry.Path));
            else folders.Add(entry.Path);
        }
    }
}
