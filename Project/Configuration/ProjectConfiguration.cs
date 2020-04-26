using System.Collections.Generic;
using System.Linq;
using CodeStatistics.Project.Files;
using CodeStatistics.Resources;

namespace CodeStatistics.Project.Configuration{
    sealed class ProjectConfiguration{
        public static ProjectConfiguration Empty { get; } = new ProjectConfiguration();
        public static ProjectConfiguration Default { get; } = ProjectConfigurationParser.FromStream(new Builder(), Embedded.DefaultConfiguration);
        
        private readonly HashSet<PathPattern> include = new HashSet<PathPattern>();
        private readonly HashSet<PathPattern> exclude = new HashSet<PathPattern>();
        private readonly Dictionary<string, FileCodeKind> associateCode = new Dictionary<string, FileCodeKind>();
        private readonly Dictionary<string, FileAssetKind> associateAsset = new Dictionary<string, FileAssetKind>();

        private ProjectConfiguration(){}

        // Properties

        public bool CheckIncludedAndNotExcluded(string relativePath){
            return (include.Count == 0 || include.Any(pattern => pattern.Matches(relativePath))) && !exclude.Any(pattern => pattern.Matches(relativePath));
        }

        public FileCodeKind? GetCodeAssociation(string extension){
            return associateCode.TryGetValue(extension, out var kind) ? kind : (FileCodeKind?)null;
        }

        public FileAssetKind? GetAssetAssociation(string extension){
            return associateAsset.TryGetValue(extension, out var kind) ? kind : (FileAssetKind?)null;
        }

        // Build

        public class Builder{
            public HashSet<PathPattern> Include { get; } = new HashSet<PathPattern>(Empty.include);
            public HashSet<PathPattern> Exclude { get; } = new HashSet<PathPattern>(Empty.exclude);
            public Dictionary<string, FileCodeKind> AssociateCode { get; } = new Dictionary<string, FileCodeKind>(Empty.associateCode);
            public Dictionary<string, FileAssetKind> AssociateAsset { get; } = new Dictionary<string, FileAssetKind>(Empty.associateAsset);

            public Builder MergeFrom(ProjectConfiguration configuration){
                Include.UnionWith(configuration.include);
                Exclude.UnionWith(configuration.exclude);
                MergeDictionary(AssociateCode, configuration.associateCode);
                MergeDictionary(AssociateAsset, configuration.associateAsset);
                return this;
            }

            public ProjectConfiguration Build(){
                var configuration = new ProjectConfiguration();

                configuration.include.UnionWith(Include);
                configuration.exclude.UnionWith(Exclude);
                MergeDictionary(configuration.associateCode, AssociateCode);
                MergeDictionary(configuration.associateAsset, AssociateAsset);

                return configuration;
            }

            private static void MergeDictionary<TKey, TValue>(Dictionary<TKey, TValue> into, Dictionary<TKey, TValue> from) where TKey : notnull{
                foreach(var (key, value) in from){
                    into[key] = value;
                }
            }
        }
    }
}
