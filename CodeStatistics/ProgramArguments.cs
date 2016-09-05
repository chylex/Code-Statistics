using System;
using System.Collections.Generic;
using CodeStatistics.Data;

namespace CodeStatistics{
    class ProgramArguments{
        public delegate bool ArgumentPredicate(Argument argument, Action<string> setError);

        private readonly HashSet<string> flags;
        private readonly Dictionary<string, string> variables;
        
        public string Error { get; private set; }
        public bool HasError { get { return Error != null; } }

        public ProgramArguments(string[] args, ArgumentPredicate argumentValidator){
            if (args.Length == 0){
                flags = null;
                variables = null;
            }
            else{
                flags = new HashSet<string>();
                variables = new Dictionary<string, string>(4);

                HashSet<string> identifiers = new HashSet<string>();
                Action<string> setError = err => Error = err;

                for(int index = 0; index < args.Length; index++){
                    if (args[index][0] == '-' && args.Length > 1){
                        string name = args[index].Substring(1);
                        string identifier = name.IndexOf(':') == -1 ? name : name.Substring(0, name.IndexOf(':'));

                        bool isVariable = index < args.Length-1 && args[index+1][0] != '-';

                        if (!identifiers.Add(identifier)){
                            Error = Lang.Get["ErrorInvalidArgsDuplicateIdentifier", identifier];
                            break;
                        }

                        if (!argumentValidator(isVariable ? new Argument(name, args[index+1]) : new Argument(name), setError)){
                            break;
                        }

                        if (isVariable)variables[name] = args[index+1];
                        else flags.Add(name);
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

        public struct Argument{
            public readonly string Name, Value;
            
            public bool IsFlag { get { return Value == null; } }

            public Argument(string name, string value = null){
                this.Name = name;
                this.Value = value;
            }
        }
    }
}
