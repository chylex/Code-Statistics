using CodeStatistics.ConsoleUtil;
using CodeStatistics.Handlers.Objects.Java.Enums;
using System;

namespace CodeStatistics.Handlers.Objects.Java.Tabs{
    class JavaGeneralTab : JavaTab{
        private int totalFiles, javaFiles, assetFiles, unknownFiles, totalPackages;
        private int totalClasses, classFiles, totalInterfaces, interfaceFiles, totalEnums, enumFiles, totalAnnotations, annotationFiles;

        public JavaGeneralTab(JavaStatistics stats) : base("General",stats){
            javaFiles = stats.FileInfo.Values.Count;

            AssetHandler assets = FileHandlers.GetByType<AssetHandler>();
            if (assets != null)assetFiles = assets.GetAssetFileCount();

            UnknownHandler unknown = FileHandlers.GetByType<UnknownHandler>();
            if (unknown != null)unknownFiles = unknown.GetUnknownFileCount();

            totalFiles = javaFiles+assetFiles+unknownFiles;

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

        public override void RenderInfo(ConsoleWrapper c, int y){
            int px, py;

            // Files
            px = c.Width/2-("File Count: ".Length+totalFiles.ToString().Length)-5;

            c.MoveTo(px,py = y);
            c.Write("File Count: ",ConsoleColor.Yellow);
            c.Write(totalFiles.ToString(),ConsoleColor.Gray);

            c.MoveTo(px,++py);
            c.Write("Java: ",ConsoleColor.White);
            c.Write(javaFiles.ToString(),ConsoleColor.Gray);

            c.MoveTo(px,++py);
            c.Write("Assets: ",ConsoleColor.White);
            c.Write(assetFiles.ToString(),ConsoleColor.Gray);

            c.MoveTo(px,++py);
            c.Write("Unknown: ",ConsoleColor.White);
            c.Write(unknownFiles.ToString(),ConsoleColor.Gray);

            // Lines & Characters
            px = c.Width/2-("Total Characters: ".Length+stats.CharactersTotal.ToString().Length)-5;

            c.MoveTo(px,py += 3);
            c.Write("Total Lines: ",ConsoleColor.Yellow);
            c.Write(stats.LinesTotal.ToString(),ConsoleColor.Gray);

            c.MoveTo(px,++py);
            c.Write("Total Characters: ",ConsoleColor.Yellow);
            c.Write(stats.CharactersTotal.ToString(),ConsoleColor.Gray);

            // Packages & Types
            px = c.Width/2+5;

            c.MoveTo(px,py = y);
            c.Write("Java Elements",ConsoleColor.Yellow);

            c.MoveTo(px,++py);
            c.Write("Packages: ",ConsoleColor.White);
            c.Write(totalPackages.ToString(),ConsoleColor.Gray);

            c.MoveTo(px,++py);
            c.Write("Classes: ",ConsoleColor.White);
            c.SetForeground(ConsoleColor.Gray);
            c.Write("{0} in {1} file{2}",totalClasses,classFiles,classFiles == 1 ? "" : "s");

            c.MoveTo(px,++py);
            c.Write("Interfaces: ",ConsoleColor.White);
            c.SetForeground(ConsoleColor.Gray);
            c.Write("{0} in {1} file{2}",totalInterfaces,interfaceFiles,interfaceFiles == 1 ? "" : "s");

            c.MoveTo(px,++py);
            c.Write("Enums: ",ConsoleColor.White);
            c.SetForeground(ConsoleColor.Gray);
            c.Write("{0} in {1} file{2}",totalEnums,enumFiles,enumFiles == 1 ? "" : "s");

            c.MoveTo(px,++py);
            c.Write("Annotations: ",ConsoleColor.White);
            c.SetForeground(ConsoleColor.Gray);
            c.Write("{0} in {1} file{2}",totalAnnotations,annotationFiles,annotationFiles == 1 ? "" : "s");
        }
    }
}
