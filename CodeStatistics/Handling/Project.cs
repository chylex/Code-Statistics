﻿using CodeStatistics.Input;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;
using CodeStatisticsCore.Handling;
using CodeStatisticsCore.Input;

// ReSharper disable AccessToModifiedClosure
namespace CodeStatistics.Handling{
    class Project{
        public delegate void ProgressEventHandler(int percentage, int processedEntries, int totalEntries);
        public delegate void FinishEventHandler(Variables variables);
        public delegate void FailureEventHandler(Exception ex);

        public event ProgressEventHandler Progress;
        public event FinishEventHandler Finish;
        public event FailureEventHandler Failure;
        private event Action CancelFinish;

        public readonly FileSearchData SearchData;

        private readonly CancellationTokenSource cancelToken;

        public Project(FileSearchData searchData){
            this.SearchData = searchData;
            this.cancelToken = new CancellationTokenSource();
        }

        public void ProcessAsync(){
            var task = new Task(() => {
                var variables = new Variables.Root();
                
                int processedEntries = 0, totalEntries = SearchData.EntryCount;
                int processedWeight = 0, totalWeight = HandlerList.GetTotalWeight(SearchData);

                int updateCounter = 0, updateInterval = totalEntries/400;

                if (Progress != null)Progress(0, processedEntries, totalEntries);

                Action checkProgress;

                if (Progress != null){
                    checkProgress = () => {
                        if (++updateCounter > updateInterval && Progress != null){
                            updateCounter = 0;
                            Progress((int)Math.Floor(100F*processedWeight/totalWeight), processedEntries, totalEntries);
                        }
                    };
                }
                else{
                    checkProgress = () => {};
                }
                
                // Folders
                List<IFolderHandler> folderHandlers = HandlerList.GetFolderHandlers().ToList();
                int folderHandlerWeight = folderHandlers.Sum(handler => handler.Weight);

                foreach(string folder in SearchData.Folders){
                    if (cancelToken.IsCancellationRequested){
                        if (CancelFinish != null)CancelFinish();
                        return;
                    }

                    foreach(IFolderHandler folderHandler in folderHandlers){
                        folderHandler.Process(folder, variables);
                    }

                    ++processedEntries;
                    ++updateCounter;
                    processedWeight += folderHandlerWeight;
                    checkProgress();
                }

                // Files
                HashSet<IFileHandler> initializedHandlers = new HashSet<IFileHandler>();

                foreach(File file in SearchData.Files){
                    if (cancelToken.IsCancellationRequested){
                        if (CancelFinish != null)CancelFinish();
                        return;
                    }

                    IFileHandler handler = HandlerList.GetFileHandler(file);

                    if (initializedHandlers.Add(handler)){
                        handler.SetupProject(variables);
                    }

                    handler.Process(file, variables);

                    ++processedEntries;
                    ++updateCounter;
                    processedWeight += handler.Weight;
                    checkProgress();
                }

                foreach(IFileHandler handler in initializedHandlers){
                    handler.FinalizeProject(variables);
                }

                // Finalize
                if (Progress != null)Progress(100, processedEntries, totalEntries);
                if (Finish != null)Finish(variables);
            }, cancelToken.Token);
            
            task.ContinueWith(originalTask => {
                if (Failure != null)Failure(originalTask.Exception);
            }, TaskContinuationOptions.OnlyOnFaulted);
            
            task.Start();
        }

        public void Cancel(Action onCancelFinish){
            cancelToken.Cancel(false);
            CancelFinish += onCancelFinish;
        }
    }
}
