using CodeStatistics.Input;

namespace CodeStatistics.Handlers.Objects{
    class UnknownHandler : FileHandler.Minor{
        private int unknownFiles;

        public UnknownHandler(int priority) : base(priority){}

        public override void Handle(File file){
            ++unknownFiles;
        }

        public int GetUnknownFileCount(){
            return unknownFiles;
        }

        public override IHandlerTab[] GenerateTabs(){
            return new IHandlerTab[0];
        }

        public override int GetWeight(){
            return 1;
        }
    }
}
