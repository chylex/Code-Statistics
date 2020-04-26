using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CodeStatistics.Project.Files;

namespace CodeStatistics.Project.Configuration{
    static class ProjectConfigurationParser{
        public const string FileName = ".codestatistics";

        public static ProjectConfiguration FromStream(ProjectConfiguration.Builder builder, Stream stream){
            var lines = new List<string>();

            using(var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true)){
                string? line;

                while((line = reader.ReadLine()) != null){
                    lines.Add(line);
                }
            }

            return FromLines(builder, lines);
        }

        public static ProjectConfiguration FromLines(ProjectConfiguration.Builder builder, IEnumerable<string> lines){
            string? currentGroup = null;

            foreach(var line in lines.Select(line => line.Trim())){
                if (string.IsNullOrEmpty(line) || line[0] == '#'){ // comments begin with #
                    continue;
                }

                if (line.Length >= 3 && line[0] == '[' && line[^1] == ']'){ // [group] merges following declarations into the group
                    currentGroup = line[1..^1].ToLowerInvariant();

                    if (currentGroup[^1] == '!'){ // [group!] completely replaces existing group declarations with the following ones
                        currentGroup = currentGroup[..^1];
                        builder.ResetGroup(currentGroup);
                    }

                    continue;
                }

                if (currentGroup == null){
                    throw new FormatException($"Declaration '{line}' does not belong to any [Group].");
                }

                builder.UpdateGroup(currentGroup, line);
            }

            return builder.Build();
        }

        private static void ResetGroup(this ProjectConfiguration.Builder builder, string group){
            switch(group){
                case "include":
                    builder.Include.Clear();
                    break;

                case "exclude":
                    builder.Exclude.Clear();
                    break;

                case "associate-code":
                    builder.AssociateCode.Clear();
                    break;

                case "associate-asset":
                    builder.AssociateAsset.Clear();
                    break;

                default:
                    throw new FormatException($"Cannot reset unknown group [{group}].");
            }
        }

        private static void UpdateGroup(this ProjectConfiguration.Builder builder, string group, string declaration){
            static (string, string) SplitInTwo(string declaration){
                var split = declaration.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (split.Length != 2){
                    throw new FormatException($"Declaration '{declaration}' must contain a key and value separated by a space.");
                }

                return (split[0], split[1]);
            }

            switch(group){
                case "include": // includes path pattern in file search (only included paths are searched; if none are included, it searches all paths without restriction)
                    builder.Include.Add(new PathPattern(declaration));
                    break;

                case "exclude": // excludes path pattern from file search (applied after includes)
                    builder.Exclude.Add(new PathPattern(declaration));
                    break;

                case "associate-code": { // associates a file extension (no dot) with a source code file type (or removes existing association if type is 'none')
                    var (key, value) = SplitInTwo(declaration);

                    if (value == "none"){
                        builder.AssociateCode.Remove(key);
                    }
                    else{
                        builder.AssociateCode[key] = FileCodeKinds.FromString(value) ?? throw new FormatException($"Invalid kind of source code file: {value}");
                    }
                    break;
                }

                case "associate-asset": { // associates a file extension (no dot) with an asset file type (or removes existing association if type is 'none')
                    var (key, value) = SplitInTwo(declaration);

                    if (value == "none"){
                        builder.AssociateAsset.Remove(key);
                    }
                    else{
                        builder.AssociateAsset[key] = FileAssetKinds.FromString(value) ?? throw new FormatException($"Invalid kind of asset file: {value}");
                    }

                    break;
                }

                default:
                    throw new FormatException($"Declaration '{declaration}' belongs to an unknown group [{group}].");
            }
        }
    }
}
