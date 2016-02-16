using System.Collections.Generic;

namespace CodeStatistics{
    class ProgramArguments{
        private readonly HashSet<string> flags;
        private readonly Dictionary<string,string> variables;

        public ProgramArguments(string[] args){
            if (args.Length == 0){
                flags = null;
                variables = null;
            }
            else{
                flags = new HashSet<string>();
                variables = new Dictionary<string,string>(4);

                for(int index = 0; index < args.Length; index++){
                    if (args[index][0] == '-' && args.Length > 1){
                        if (index < args.Length-1 && args[index+1][0] != '-'){
                            variables[args[index].Substring(1)] = args[index+1];
                        }
                        else{
                            flags.Add(args[index].Substring(1));
                        }
                    }
                }
            }
        }

        public bool CheckFlag(string name){
            return flags != null && flags.Contains(name);
        }

        public bool HasVariable(string name){
            return variables != null && variables.ContainsKey(name);
        }

        public string GetVariable(string name){
            return variables[name];
        }
    }
}
