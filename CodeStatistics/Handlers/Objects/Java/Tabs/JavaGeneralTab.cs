using CodeStatistics.Handlers.Objects.Java.Enums;

namespace CodeStatistics.Handlers.Objects.Java.Tabs{
    class JavaGeneralTab{
        private readonly int totalFiles, javaFiles, assetFiles, unknownFiles, totalPackages;
        private readonly int totalClasses, classFiles, totalInterfaces, interfaceFiles, totalEnums, enumFiles, totalAnnotations, annotationFiles;

        public JavaGeneralTab(JavaStatistics stats){
            javaFiles = stats.FileInfo.Values.Count;

            /*AssetHandler assets = FileHandlers.GetByType<AssetHandler>();
            if (assets != null)assetFiles = assets.GetAssetFileCount();

            UnknownHandler unknown = FileHandlers.GetByType<UnknownHandler>();
            if (unknown != null)unknownFiles = unknown.GetUnknownFileCount();

            totalFiles = javaFiles+assetFiles+unknownFiles;*/

            totalPackages = stats.Packages.Count;
            totalClasses = stats.TypeCounts[JavaType.Class];
            classFiles = stats.TypeFileCounts[JavaType.Class];
            totalInterfaces = stats.TypeCounts[JavaType.Interface];
            interfaceFiles = stats.TypeFileCounts[JavaType.Interface];
            totalEnums = stats.TypeCounts[JavaType.Enum];
            enumFiles = stats.TypeFileCounts[JavaType.Enum];
            totalAnnotations = stats.TypeCounts[JavaType.Annotation];
            annotationFiles = stats.TypeFileCounts[JavaType.Annotation];
        }
    }
}
