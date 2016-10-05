using System.Collections.Generic;
using CodeStatisticsCore.Input;

namespace CodeStatistics.Input{
    class FileSearchData{
        private readonly HashSet<File> files = new HashSet<File>();
        private readonly HashSet<string> folders = new HashSet<string>();

        public readonly string Root;

        public IEnumerable<File> Files{
            get { return files; }
        }

        public IEnumerable<string> Folders{
            get { return folders; }
        }

        public int EntryCount{
            get { return files.Count+folders.Count; }
        }

        public FileSearchData(string root){
            this.Root = root;
        }

        public void Add(IOEntry entry){
            if (entry.EntryType == IOEntry.Type.File){
                files.Add(new File(entry.Path));
            }
            else{
                folders.Add(entry.Path);
            }
        }
    }
}
