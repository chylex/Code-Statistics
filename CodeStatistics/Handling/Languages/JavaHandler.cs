using CodeStatistics.Input;

namespace CodeStatistics.Handling.Languages {
    class JavaHandler : FileHandler{
        public override int Weight{
            get { return 50; }
        }

        public override void Process(File file, Variables.Root variables){
            
        }
    }
}
