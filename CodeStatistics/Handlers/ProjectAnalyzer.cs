using CodeStatistics.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeStatistics.Handlers{
    class ProjectAnalyzer{
        public delegate void UpdateEventHandler(float percentage, int handledFiles, int totalFiles);

        public event UpdateEventHandler Update;

        private readonly ICollection<File> files;

        public ProjectAnalyzer(ICollection<File> files){
            this.files = files;
        }

        public List<IHandlerTab> Run(){
            int updateInterval = (int)Math.Ceiling(files.Count/200D), nextUpdate = updateInterval;
            int handledFiles = 0, totalFiles = files.Count;
            HashSet<FileHandler> handlers = new HashSet<FileHandler>();

            if (Update != null)Update(0F,0,totalFiles);

            foreach(File file in files){
                FileHandler handler = FileHandlers.Get(file.Ext);

                if (handler != null && handler.IsFileValid(file)){
                    handler.Handle(file);
                    handlers.Add(handler);
                }

                ++handledFiles;

                if (--nextUpdate <= 0){
                    nextUpdate = updateInterval;
                    if (Update != null)Update((float)handledFiles/totalFiles,handledFiles,totalFiles);
                }
            }

            if (Update != null)Update(1F,totalFiles,totalFiles);

            // Major handler check
            int majorHandlerCount = handlers.Count(handler => handler is FileHandler.Major);

            if (majorHandlerCount > 1){
                // TODO
            }

            // Tab list
            List<IHandlerTab> tabs = new List<IHandlerTab>();
            if (majorHandlerCount > 0)tabs.AddRange(handlers.Where(handler => handler is FileHandler.Major).First().GenerateTabs());

            List<FileHandler> minorHandlers = handlers.Where(handler => handler is FileHandler.Minor).ToList();
            minorHandlers.Sort();
            minorHandlers.ForEach(handler => tabs.AddRange(handler.GenerateTabs()));

            return tabs;
        }
    }
}
