using CodeStatistics.ConsoleUtil;

namespace CodeStatistics.Handlers.Objects.Java.Tabs{
    class JavaGeneralTab : JavaTab{
        private int totalFiles, javaFiles, assetFiles, unknownFiles;

        public JavaGeneralTab(JavaStatistics stats) : base("General",stats){
            javaFiles = stats.FileInfo.Values.Count;

            AssetHandler assets = FileHandlers.GetByType<AssetHandler>();
            if (assets != null)assetFiles = assets.GetAssetFileCount();

            UnknownHandler unknown = FileHandlers.GetByType<UnknownHandler>();
            if (unknown != null)unknownFiles = unknown.GetUnknownFileCount();

            totalFiles = javaFiles+assetFiles+unknownFiles;
        }

        public override void RenderInfo(ConsoleWrapper c, int y){
            
        }
    }
}
