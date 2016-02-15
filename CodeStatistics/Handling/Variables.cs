using System.Collections.Generic;

namespace CodeStatistics.Handling{
    class Variables{
        public bool CheckFlag(string name){
            return false;
        }

        public string GetVariable(string name){
            return "";
        }

        public IEnumerable<Variables> GetArray(string name){
            yield break;
        }
    }
}
