using System;
using System.IO;
using System.Linq;

namespace CodeStatistics.Project.Files{
    sealed class PathPattern{
        private const char Separator = '/';

        private readonly string[] parts;
        private readonly bool isAbsolute;

        public PathPattern(string pattern){
            this.parts = NormalizeDirectorySeparator(pattern).Split(Separator, StringSplitOptions.RemoveEmptyEntries).SkipWhile(item => item == ".").ToArray();
            
            if (parts.Length == 0){
                this.isAbsolute = true;
            }
            else if (parts[0] == "~"){ // '~/dir' matches 'dir' in the project root folder
                this.isAbsolute = true;
                this.parts = parts.Skip(1).ToArray();
            }
            else{
                this.isAbsolute = false;
            }
        }

        public bool Matches(string relativePath){
            ArraySegment<string> split = NormalizeDirectorySeparator(relativePath).Split(Separator);
            int lastSkip = isAbsolute ? 0 : split.Count - 1;

            for(int skip = 0; skip <= lastSkip; skip++){
                if (MatchesPart(split.Slice(skip))){
                    return true;
                }
            }

            return false;
        }

        private bool MatchesPart(ArraySegment<string> split){
            for(int i = 0; i < split.Count; i++){
                if (i == parts.Length){
                    return parts[^1] == "*"; // 'dir/*' matches 'dir/sub/file.txt'
                }

                if (split[i] != parts[i]){
                    if (parts[i] == "*"){
                        continue; // 'dir/*' matches 'dir/file.txt'
                    }
                    else if (i == parts.Length - 1 && parts[i].StartsWith("*.") && Path.GetExtension(split[^1]) == parts[i][1..]){
                        continue; // 'dir/*.txt' matches 'dir/file.txt'
                    }

                    return false; // mismatched pattern that is not a supported wildcard causes failure
                }
            }

            return true;
        }

        public override bool Equals(object? obj){
            return obj is PathPattern other && parts.SequenceEqual(other.parts) && isAbsolute == other.isAbsolute;
        }

        public override int GetHashCode(){
            int hashCode = isAbsolute ? 2 : 1;

            foreach(var part in parts){
                hashCode = HashCode.Combine(hashCode, part);
            }

            return hashCode;
        }

        public override string ToString(){
            return (isAbsolute ? "~/" : "") + string.Join(Separator, parts);
        }

        public static string NormalizeDirectorySeparator(string path){
            return path.Replace(Path.DirectorySeparatorChar, '/')
                       .Replace(Path.AltDirectorySeparatorChar, '/');
        }
    }
}
