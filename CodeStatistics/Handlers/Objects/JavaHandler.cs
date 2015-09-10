using CodeStatistics.ConsoleUtil;
using CodeStatistics.Handlers.Objects.Java;
using CodeStatistics.Input;

namespace CodeStatistics.Handlers.Objects{
    class JavaHandler : FileHandler.Major{
        private JavaStatistics stats = new JavaStatistics();

        public override string GetName(){
            return "Java";
        }

        public override void Handle(File file){
            JavaParser.Parse(file.Read(),stats);
        }

        public override IHandlerTab[] GenerateTabs(){
            return JavaTab.GenerateTabs(stats);
        }

        public override int GetWeight(){
            return 20;
        }
    }
}
