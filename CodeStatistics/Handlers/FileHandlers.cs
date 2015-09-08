using CodeStatistics.Handlers.Objects;
using System.Collections.Generic;

namespace CodeStatistics.Handlers{
    static class FileHandlers{
        private static readonly Dictionary<string,FileHandler> handlers = new Dictionary<string,FileHandler>();

        static FileHandlers(){
            AssetHandler assetHandler = new AssetHandler(10);

            foreach(string ext in new string[]{
                "jpg", "jpeg", "gif", "png", "bmp", "ico", "icns", "tif", "tiff", "tga", "svg"
            })handlers.Add(ext,assetHandler.SetType(ext,AssetHandler.Type.Image));

            foreach(string ext in new string[]{
                "wav", "mp3", "ogg", "flac", "aiff", "wma", "m4a", "aac", "mid", "mod"
            })handlers.Add(ext,assetHandler.SetType(ext,AssetHandler.Type.Audio));

            foreach(string ext in new string[]{
                "mp4", "mkv", "mpg", "mpeg", "avi", "m4v", "mov", "wmv"
            })handlers.Add(ext,assetHandler.SetType(ext,AssetHandler.Type.Video));

            foreach(string ext in new string[]{
                "txt", "md", "rtf", "pdf", "doc", "docx", "odt"
            })handlers.Add(ext,assetHandler.SetType(ext,AssetHandler.Type.Text));

            foreach(string ext in new string[]{
                "json", "xml", "conf", "cfg", "ini", "yaml"
            })handlers.Add(ext,assetHandler.SetType(ext,AssetHandler.Type.Configuration));

            foreach(string ext in new string[]{
                "zip", "rar", "tar", "gz", "tgz", "cab", "bz2", "bzip", "lz", "lzma", "arc", "pak"
            })handlers.Add(ext,assetHandler.SetType(ext,AssetHandler.Type.Archive));
        }

        public static FileHandler Get(string extNoDot){
            FileHandler handler;
            return handlers.TryGetValue(extNoDot,out handler) ? handler : null;
        }
    }
}

