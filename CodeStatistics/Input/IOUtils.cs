using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileIO = System.IO.File;

namespace CodeStatistics.Input{
    public static class IOUtils{
        /// <summary>
        /// Finds the longest common path in an array. Does not include the trailing path separator, except when only the drive letter is returned. <para/>
        /// If a path includes a file, the returned path will also include it if it's present in all other paths. <para/>
        /// 
        /// For example, <code>FindRootPath({ "C:\Projects\One", "C:\Projects\Two" })</code> returns C:\Projects
        /// </summary>
        public static string FindRootPath(string[] paths){
            if (paths.Length == 0)throw new ArgumentException("Array must contain at least one element.","paths");
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
    }
}
