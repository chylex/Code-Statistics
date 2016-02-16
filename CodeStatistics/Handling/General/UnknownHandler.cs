using CodeStatistics.Input;

namespace CodeStatistics.Handling.General{
    class UnknownHandler : FileHandler{
        public override int Weight{
            get { return 1; }
        }

        public override void Process(File file, Variables.Root variables){
            
        }
    }
}
