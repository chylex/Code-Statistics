using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeStatistics.Data;
using FileIO = System.IO.File;

namespace CodeStatistics.Input{
    public static class IOUtils{
        private readonly static string[] FileSizeSuffixes = { "B", "kB", "MB", "GB", "TB", "PB", "EB" };

        /// <summary>
        /// Finds the longest common path in an array. Does not include the trailing path separator, except when only the drive letter is returned. <para/>
        /// If a path includes a file, the returned path will also include it if it's present in all other paths. <para/>
        /// 
        /// For example, <code>FindRootPath({ "C:\Projects\One", "C:\Projects\Two" })</code> returns C:\Projects
        /// </summary>
        public static string FindRootPath(string[] paths){
            if (paths.Length == 0)throw new ArgumentException(Lang.Get["ErrorArgumentArrayAtLeastOne"],"paths");
            if (paths.Length == 1)return paths[0];

            List<string[]> split = paths.Select(path => path.Split(new []{ Path.DirectorySeparatorChar },StringSplitOptions.RemoveEmptyEntries)).ToList();
            int commonElements = 0, maxCount = split.Min(path => path.Length);

            for(; commonElements < maxCount; commonElements++){
                string testElement = split[0][commonElements];

                if (!split.Skip(1).All(path => path[commonElements].Equals(testElement))){
                    break;
                }
            }

            string commonPath = string.Join(Path.DirectorySeparatorChar.ToString(),split[0].Take(commonElements));
            return commonElements == 1 && commonPath[commonPath.Length-1] == ':' ? commonPath+Path.DirectorySeparatorChar : commonPath;
        }

        /// <summary>
        /// Returns true if the path points to an existing directory, false if it points to a file, and null if an exception happens during the IO operations.
        /// </summary>
        public static bool? IsDirectory(string path){
            try{
                return FileIO.GetAttributes(path).HasFlag(FileAttributes.Directory);
            }catch(Exception){
                return null;
            }
        }

        /// <summary>
        /// Checks whether file extension matches. The <paramref name="extension"/> parameter is provided without the dot and ignores case.
        /// </summary>
        public static bool CheckExtension(string path, string extension){
            string fileExt = Path.GetExtension(path);
            return fileExt == null || fileExt == "." ? extension.Length == 0 : string.Equals(fileExt,extension,StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Creates a randomly named directory in %TEMP%/CSTMP and returns the full path, or null if the creation fails.
        /// </summary>
        public static string CreateTemporaryDirectory(){
            string path = Path.Combine(GetTempRoot(),"CSTMP",Path.GetRandomFileName());
            
            try{
                Directory.CreateDirectory(path);
                return path;
            }catch(IOException){
                return null;
            }
        }

        /// <summary>
        /// Cleans up leftover files in the temporary directory, and deletes it. Returns true on success.
        /// </summary>
        public static bool CleanupTemporaryDirectory(){
            try{
                Directory.Delete(Path.Combine(GetTempRoot(),"CSTMP"),true);
                return true;
            }catch(Exception){
                return false;
            }
        }

        /// <summary>
        /// Returns a friendly representation of file size, going from B to EB.
        /// </summary>
        public static KeyValuePair<long,string> GetFriendlyFileSize(long bytes){
            int level = bytes == 0 ? 0 : (int)Math.Floor(Math.Log(bytes,1024));
            return new KeyValuePair<long,string>((long)Math.Round(bytes/Math.Pow(1024,level)),FileSizeSuffixes[level]);
        }

        /// <summary>
        /// Returns the temporary folder for currently running system.
        /// </summary>
        private static string GetTempRoot(){
#if WINDOWS
            return Environment.ExpandEnvironmentVariables("%TEMP%");
#else
            return Path.GetTempPath();
#endif
        }
    }
}
