using CodeStatistics.Handling.General;
using CodeStatistics.Handling.Languages;
using CodeStatistics.Input;
using System.Collections.Generic;
using System.Linq;
using CodeStatisticsCore.Handling;
using CodeStatisticsCore.Input;

namespace CodeStatistics.Handling{
    static class HandlerList{
        private static readonly Dictionary<string,IFileHandler> FileHandlers = new Dictionary<string,IFileHandler>(8);
        private static readonly IFileHandler UnknownFileHandler;

        private static readonly List<IFolderHandler> FolderHandlers = new List<IFolderHandler>(1);
        private static int folderHandlerWeight = 0;

        static HandlerList(){
            UnknownFileHandler = new UnknownHandler();

            AddFileHandler(new JavaHandler(),new []{ "java", "jav" });

            AddFileHandler(new AssetHandler(AssetHandler.Type.Image),new []{ "jpg", "jpeg", "gif", "png", "bmp", "ico", "icns", "tif", "tiff", "tga", "svg" });
            AddFileHandler(new AssetHandler(AssetHandler.Type.Audio),new []{ "wav", "mp3", "ogg", "flac", "aiff", "wma", "m4a", "aac", "mid", "mod" });
            AddFileHandler(new AssetHandler(AssetHandler.Type.Video),new []{ "mp4", "mkv", "mpg", "mpeg", "avi", "m4v", "mov", "wmv" });
            AddFileHandler(new AssetHandler(AssetHandler.Type.Document),new []{ "txt", "md", "rtf", "pdf", "doc", "docx", "odt" });
            AddFileHandler(new AssetHandler(AssetHandler.Type.Configuration),new []{ "json", "xml", "conf", "cfg", "ini", "yaml" });
            AddFileHandler(new AssetHandler(AssetHandler.Type.Archive),new []{ "zip", "rar", "tar", "gz", "tgz", "cab", "bz2", "bzip", "lz", "lzma", "arc", "pak" });

            AddFolderHandler(new FolderHandler());
        }

        private static void AddFileHandler(IFileHandler handler, string extension){
            FileHandlers.Add(extension,handler);
        }

        private static void AddFileHandler(IFileHandler handler, string[] extensions){
            foreach(string ext in extensions)FileHandlers.Add(ext,handler);
        }

        private static void AddFolderHandler(IFolderHandler handler){
            FolderHandlers.Add(handler);
            folderHandlerWeight += handler.Weight;
        }

        public static IFileHandler GetFileHandler(File file){
            IFileHandler handler;
            return FileHandlers.TryGetValue(file.Ext,out handler) ? handler : UnknownFileHandler;
        }

        public static IEnumerable<IFolderHandler> GetFolderHandlers(){
            return FolderHandlers;
        }

        public static int GetTotalWeight(FileSearchData searchData){
            return searchData.Files.Sum(file => GetFileHandler(file).Weight)+searchData.Folders.Count()*folderHandlerWeight;
        }
    }
}
