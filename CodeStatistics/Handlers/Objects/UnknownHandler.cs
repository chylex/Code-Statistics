using CodeStatistics.Input;
using System;

namespace CodeStatistics.Handlers.Objects{
    class UnknownHandler : FileHandler.Minor{
        private int unknownFiles = 0;

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
