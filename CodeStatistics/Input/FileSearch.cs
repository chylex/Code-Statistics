using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeStatistics.Input{
    class FileSearch{
        public delegate void RefreshEventHandler(int filesFound);
        public delegate void FinishEventHandler(HashSet<File> files);

        public event RefreshEventHandler Refresh;
        public event FinishEventHandler Finish;

        private string[] rootFiles;

        public FileSearch(string[] files){
            this.rootFiles = files;
        }

        public void Search(){
            Task<HashSet<File>> searchTask = Task<HashSet<File>>.Factory.StartNew(() => {
                HashSet<File> foundFiles = new HashSet<File>();
                Random rand = new Random();
                int fileCount = 0, nextNotice = 0;

                Action updateNotice = () => {
                    if (--nextNotice < 0){
                        if (Refresh != null)Refresh(fileCount);
                        nextNotice = 50+rand.Next(40);
                    }
                };

                updateNotice();

                foreach(string rootFile in rootFiles){ // TODO if there are too many files, allow user to stop the search
                    if (System.IO.File.GetAttributes(rootFile).HasFlag(System.IO.FileAttributes.Directory)){
                        foreach(string file in System.IO.Directory.EnumerateFiles(rootFile,"*.*",System.IO.SearchOption.AllDirectories)){
                            foundFiles.Add(new File(file));
                            ++fileCount;
                            updateNotice();
                        }
                    }
                    else{
                        foundFiles.Add(new File(rootFile));
                        ++fileCount;
                        updateNotice();
                    }
                }

                if (Refresh != null)Refresh(foundFiles.Count);
                return foundFiles;
            });

            if (Finish != null)Finish(searchTask.Result);
        }
    }
}
