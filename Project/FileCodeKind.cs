namespace CodeStatistics.Project{
    enum FileCodeKind{
        Generic
    }

    static class FileCodeKinds{
        public static FileCodeKind? FromString(string str){
            return str.ToLowerInvariant() switch{
                "generic" => FileCodeKind.Generic,
                _         => (FileCodeKind?)null
            };
        }
    }
}
