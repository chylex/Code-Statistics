using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CodeStatisticsCore.Collections;

namespace CodeStatisticsCore.Handling{
    public abstract class Variables{
        private static readonly NumberFormatInfo NumberFormat = new NumberFormatInfo{
            NumberGroupSeparator = " ",
            NumberDecimalSeparator = ".",
            NumberDecimalDigits = 1
        };

        public abstract bool CheckFlag(string name);
        public abstract string GetVariable(string name, string defaultValue);
        public abstract int GetVariable(string name, int defaultValue);
        public abstract IEnumerable<Variables> GetArray(string name);

        public class Root : Variables{
            private readonly HashSet<string> flags = new HashSet<string>();
            private readonly Dictionary<string, string> variables = new Dictionary<string, string>();
            private readonly Dictionary<string, int> variablesInt = new Dictionary<string, int>();
            private readonly Dictionary<string, List<Variables>> arrays = new Dictionary<string, List<Variables>>(4);
            private readonly Dictionary<string, Comparison<Variables>> arraySorters = new Dictionary<string, Comparison<Variables>>(4);
            private readonly Dictionary<object, object> stateObjects = new Dictionary<object, object>(4);
            
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
                variables[name] = value.ToString("N0", NumberFormat);
            }

            public void SetVariable(string name, float value){
                variablesInt[name] = (int)Math.Round(value);
                variables[name] = value.ToString("#,0.#", NumberFormat);
            }

            public void Increment(string name, int amount = 1){
                if (amount == 0)return;

                if (variablesInt.ContainsKey(name)){
                    variables[name] = (variablesInt[name] += amount).ToString("N0", NumberFormat);
                }
                else{
                    variables[name] = amount.ToString("N0", NumberFormat);
                    variablesInt[name] = amount;
                }
            }

            public override string GetVariable(string name, string defaultValue){
                string value;
                return variables.TryGetValue(name, out value) ? value : defaultValue;
            }

            public override int GetVariable(string name, int defaultValue){
                int value;
                return variablesInt.TryGetValue(name, out value) ? value : defaultValue;
            }

            public void AddStateObject(object owner, object obj){
                stateObjects.Add(owner, obj);
            }

            public T GetStateObject<T>(object owner) where T : class{
                object obj;
                return stateObjects.TryGetValue(owner, out obj) ? obj as T : null;
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

                ArrayAdapter arrayVar = new ArrayAdapter(this, array);
                arrayList.Add(arrayVar);
                return arrayVar;
            }

            public override IEnumerable<Variables> GetArray(string name){
                List<Variables> arrayList;

                if (arrays.TryGetValue(name, out arrayList)){
                    Comparison<Variables> comparer;

                    if (arraySorters.TryGetValue(name, out comparer)){
                        arrayList.Sort(comparer);
                    }

                    return arrayList;
                }
                else return Enumerable.Empty<Variables>();
            }

            public void SetArraySorter(string name, Comparison<Variables> comparer){
                arraySorters[name] = comparer;
            }
        }

        public class ArrayAdapter : Variables{
            private static readonly AnonymousDictionary.ToStringFunction ToStringFormat = o => o is int ? ((int)o).ToString("N0", NumberFormat) : o.ToString();

            private readonly Variables parent;
            private readonly Dictionary<string, string> variables;

            public ArrayAdapter(Variables parent, object array){
                this.parent = parent;
                this.variables = AnonymousDictionary.Create(array, ToStringFormat);
            }

            public override bool CheckFlag(string name){
                return parent.CheckFlag(name);
            }

            public void UpdateVariable(string name, string newValue){
                if (!variables.ContainsKey(name))throw new ArgumentException("Variable "+name+" not found in the array adapter.");
                variables[name] = newValue;
            }

            public void UpdateVariable(string name, int newValue){
                UpdateVariable(name, newValue.ToString("N0", NumberFormat));
            }

            public override string GetVariable(string name, string defaultValue){
                string value;
                return variables.TryGetValue(name, out value) ? value : defaultValue;
            }

            public override int GetVariable(string name, int defaultValue){
                string strValue = GetVariable(name, defaultValue.ToString(NumberFormat));

                int intValue;
                return int.TryParse(strValue, NumberStyles.AllowThousands, NumberFormat, out intValue) ? intValue : defaultValue;
            }

            public override IEnumerable<Variables> GetArray(string name){
                return parent.GetArray(name);
            }
        }

        public class Dummy : Variables{
            public override bool CheckFlag(string name){
                return true;
            }

            public override string GetVariable(string name, string defaultValue){
                return defaultValue;
            }

            public override int GetVariable(string name, int defaultValue){
                return defaultValue;
            }

            public override IEnumerable<Variables> GetArray(string name){
                yield return new Dummy();
            }
        }
    }
}
