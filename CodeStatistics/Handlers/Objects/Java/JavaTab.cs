using CodeStatistics.ConsoleUtil;
using CodeStatistics.Handlers.Objects.Java.Tabs;

namespace CodeStatistics.Handlers.Objects.Java{
    abstract class JavaTab : IHandlerTab{
        public static JavaTab[] GenerateTabs(JavaStatistics stats){
            return new JavaTab[]{
                new JavaGeneralTab(stats),
                new JavaNamesTab(stats),
                new JavaSyntaxTab(stats),
                new JavaFilesTab(stats),
                new JavaPrimitivesTab(stats)
            };
        }

        private readonly string name;
        protected readonly JavaStatistics stats;

        public JavaTab(string name, JavaStatistics stats){
            this.name = name;
            this.stats = stats;
        }

        public string GetName(){
            return name;
        }

        public abstract void RenderInfo(ConsoleWrapper c, int y);
    }
}
