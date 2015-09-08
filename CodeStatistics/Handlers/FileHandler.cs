using CodeStatistics.ConsoleUtil;
using CodeStatistics.Input;
using System;

namespace CodeStatistics.Handlers{
    abstract class FileHandler{
        public abstract class Minor : FileHandler, IComparable<Minor>{
            private readonly int priority;

            public Minor(int priority){
                this.priority = priority;
            }

            public int CompareTo(Minor other){
                return priority-other.priority;
            }
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
