using CodeStatistics.Handling.General;
using System.Collections.Generic;

namespace CodeStatistics.Handling{
    class HandlerList{
        public interface IWeightedEntry{
            int Weight { get; }
        }

        private static readonly Dictionary<string,IFileHandler> fileHandlers = new Dictionary<string,IFileHandler>(8);
        private static readonly List<IFolderHandler> folderHandlers = new List<IFolderHandler>(1);

       static HandlerList(){
        }
    }
}
