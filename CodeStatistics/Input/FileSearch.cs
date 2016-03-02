using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FileIO = System.IO.File;
using DirectoryIO = System.IO.Directory;
using CodeStatistics.Input.Methods;
using CodeStatistics.Forms;

namespace CodeStatistics.Input{
    public class FileSearch : IInputMethod{
        public delegate void RefreshEventHandler(int entriesFound);
        public delegate void FinishEventHandler(FileSearchData searchData);
        public delegate void FailureEventHandler(Exception ex);

        public event RefreshEventHandler Refresh;
        public event FinishEventHandler Finish;
        public event FailureEventHandler Failure;
        private event Action CancelFinish;

        private readonly string[] rootFiles;
        private readonly CancellationTokenSource cancelToken;

        public FileSearch(string[] files){
            this.rootFiles = files;
            cancelToken = new CancellationTokenSource();
        }

        public void StartAsync(){
            new Task(() => {
                var searchData = new FileSearchData(rootFiles.Length == 0 ? string.Empty : IOUtils.FindRootPath(rootFiles));
                var rand = new Random();

                int[] entryCount = { 0 };
                int nextNotice = 0;

                Action updateNotice = () => {
                    if (--nextNotice < 0){
                        if (Refresh != null)Refresh(entryCount[0]);
                        nextNotice = 100+rand.Next(50);
                    }
                };

                updateNotice();

                foreach(string rootFile in rootFiles){
                    if (cancelToken.IsCancellationRequested){
                        if (CancelFinish != null)CancelFinish();
                        return;
                    }

                    bool? isDirectory = IOUtils.IsDirectory(rootFile);
                    if (!isDirectory.HasValue)continue;

                    if (isDirectory.Value){
                        foreach(IOEntry entry in EnumerateEntriesSafe(rootFile)){
                            if (cancelToken.IsCancellationRequested){
                                if (CancelFinish != null)CancelFinish();
                                return;
                            }

                            searchData.Add(entry);
                            ++entryCount[0];
                            updateNotice();
                        }
                    }
                    else{
                        searchData.Add(new IOEntry(IOEntry.Type.File,rootFile));
                        ++entryCount[0];
                        updateNotice();
                    }
                }

                if (Refresh != null)Refresh(entryCount[0]);
                if (Finish != null)Finish(searchData);
            },cancelToken.Token).ContinueWith(task => {
                if (Failure != null)Failure(task.Exception);
            },TaskContinuationOptions.OnlyOnFaulted).Start();
        }

        private static IEnumerable<IOEntry> EnumerateEntriesSafe(string path){
            var foldersLeft = new Queue<string>(64);
            foldersLeft.Enqueue(path);

            while(foldersLeft.Count > 0){
                string currentFolder = foldersLeft.Dequeue();
                string[] files, folders;

                try{
                    files = DirectoryIO.GetFiles(currentFolder,"*.*",SearchOption.TopDirectoryOnly);
                }catch(Exception){
                    files = new string[0];
                }

                try{
                    folders = DirectoryIO.GetDirectories(currentFolder,"*.*",SearchOption.TopDirectoryOnly);
                }catch(Exception){
                    folders = new string[0];
                }

                foreach(string file in files){
                    yield return new IOEntry(IOEntry.Type.File,file);
                }

                foreach(string folder in folders){
                    foldersLeft.Enqueue(folder);
                    yield return new IOEntry(IOEntry.Type.Folder,folder);
                }
            }
        }

        public void BeginProcess(ProjectLoadForm.UpdateCallbacks callbacks){
            callbacks.OnReady(this);
        }

        public void CancelProcess(Action onCancelFinish){
            cancelToken.Cancel(false);
            CancelFinish += onCancelFinish;
        }
    }
}
