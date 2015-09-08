using CodeStatistics.ConsoleUtil;
using CodeStatistics.Input;

namespace CodeStatistics.Handlers{
    abstract class FileHandler{
        public abstract class Minor : FileHandler{
            public Minor(){}
        }

        public abstract class Major : FileHandler{
            public Major(){}

            public abstract string GetName();
        }

        public abstract void Handle(File file);

        public abstract IHandlerTab[] GenerateTabs();

        /// <summary>
        /// Can further determine whether the file is valid by analyzing the contents.
        /// </summary>
        public bool IsFileValid(File file){
            return true;
        }

        private FileHandler(){}
    }
}
