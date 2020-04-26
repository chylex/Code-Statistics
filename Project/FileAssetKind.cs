using System;

namespace CodeStatistics.Project{
    enum FileAssetKind{
        Audio,
        Video,
        Image,
        Model,
        Document,
        Font,
        Other
    }

    static class FileAssetKinds{
        public static FileAssetKind? FromString(string str){
            return str.ToLowerInvariant() switch{
                "audio"    => FileAssetKind.Audio,
                "video"    => FileAssetKind.Video,
                "image"    => FileAssetKind.Image,
                "model"    => FileAssetKind.Model,
                "document" => FileAssetKind.Document,
                "font"     => FileAssetKind.Font,
                "other"    => FileAssetKind.Other,
                _          => (FileAssetKind?)null
            };
        }

        public static string ToString(this FileAssetKind kind){
            return kind switch{
                FileAssetKind.Audio    => "audio",
                FileAssetKind.Video    => "video",
                FileAssetKind.Image    => "image",
                FileAssetKind.Model    => "model",
                FileAssetKind.Document => "document",
                FileAssetKind.Font     => "font",
                FileAssetKind.Other    => "other",
                _                      => throw new InvalidOperationException($"Invalid FileAssetKind: {kind}")
            };
        }
    }
}
