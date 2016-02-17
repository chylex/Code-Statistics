using CodeStatistics.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeStatistics.Handling{
    abstract class Variables{
        public abstract bool CheckFlag(string name);
        public abstract string GetVariable(string name, string defaultValue);
        public abstract IEnumerable<Variables> GetArray(string name);

        public class Root : Variables{
            private readonly List<string> flags = new List<string>(4);
            private readonly Dictionary<string,string> variables = new Dictionary<string,string>();
            private readonly Dictionary<string,int> variablesInt = new Dictionary<string,int>();
            private readonly Dictionary<string,List<Variables>> arrays = new Dictionary<string,List<Variables>>();
            private readonly Dictionary<object,object> stateObjects = new Dictionary<object,object>(4);
            
            public void AddFlag(string name){
                flags.Add(name);
            }

            public override bool CheckFlag(string name){
                return flags.Contains(name);
            }

            public void SetVariable(string name, string value){
                variables[name] = value;
            }

            public void SetVariable(string name, int value){
                variablesInt[name] = value;
                variables[name] = value.ToString();
            }

            public void Increment(string name){
                if (variablesInt.ContainsKey(name)){
                    variables[name] = (++variablesInt[name]).ToString();
                }
                else{
                    variables[name] = "1";
                    variablesInt[name] = 1;
                }
            }

            public override string GetVariable(string name, string defaultValue){
                string value;
                return variables.TryGetValue(name,out value) ? value : defaultValue;
            }

            public void AddStateObject(object owner, object obj){
                stateObjects.Add(owner,obj);
            }

            public T GetStateObject<T>(object owner) where T : class{
                object obj;
                return stateObjects.TryGetValue(owner,out obj) ? obj as T : null;
            }

            public ArrayAdapter AddToArray(string name, object array){
                List<Variables> arrayList;

                if (arrays.ContainsKey(name)){
                    arrayList = arrays[name];
                }
                else{
                    arrayList = new List<Variables>(4);
                    arrays[name] = arrayList;
                }

                ArrayAdapter arrayVar = new ArrayAdapter(this,array);
                arrayList.Add(arrayVar);
                return arrayVar;
            }

            public override IEnumerable<Variables> GetArray(string name){
                List<Variables> arrayList;
                return arrays.TryGetValue(name,out arrayList) ? arrayList : Enumerable.Empty<Variables>();
            }
        }

        public class ArrayAdapter : Variables{
            private readonly Variables parent;
            private readonly Dictionary<string,string> variables;

            public ArrayAdapter(Variables parent, object array){
                this.parent = parent;
                this.variables = AnonymousDictionary.Create<string>(array);
            }

            public override bool CheckFlag(string name){
                return parent.CheckFlag(name);
            }

            public override string GetVariable(string name, string defaultValue){
                string value;
                return variables.TryGetValue(name,out value) ? value : defaultValue;
            }

            public override IEnumerable<Variables> GetArray(string name){
                return parent.GetArray(name);
            }
        }
    }
}
