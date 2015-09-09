using CodeStatistics.ConsoleUtil;

namespace CodeStatistics.Handlers.Objects.Java{
    static class JavaTabs{
        public static JavaTab[] GenerateTabs(JavaStatistics stats){
            return new JavaTab[]{
                new GeneralTab(stats),
                new NamesTab(stats)
            };
        }

        public abstract class JavaTab : IHandlerTab{
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

        class GeneralTab : JavaTab{
            public GeneralTab(JavaStatistics stats) : base("General",stats){}

            public override void RenderInfo(ConsoleWrapper c, int y){

            }
        }

        class NamesTab : JavaTab{
            public NamesTab(JavaStatistics stats) : base("Names",stats){}

            public override void RenderInfo(ConsoleWrapper c, int y){

            }
        }
    }
}
