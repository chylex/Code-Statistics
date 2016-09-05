using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using CodeStatistics.Data;
using CodeStatisticsCore.Handling;

namespace CodeStatistics.Output{
    abstract class TemplateToken{
        private static readonly Regex MatchToken = new Regex(@"{(\w+(?::[\w\s]+)*)}", RegexOptions.Compiled);
        private static readonly char[] DynamicValueSplit = { ':' };

        public static IEnumerable<TemplateToken> FindTokens(string text){
            foreach(Match match in MatchToken.Matches(text)){
                TemplateToken token = FromContent(match.Index, match.Length, match.Groups[1].Value);

                if (token == null)throw new TemplateException(Lang.Get["TemplateErrorUnknownToken", match.Groups[1].Value]);
                else yield return token;
            }
        }

        private static TemplateToken FromContent(int indexStart, int indexEnd, string declaration){
            if (declaration.IndexOf(':') != -1){
                string[] dynamic = declaration.Split(DynamicValueSplit, 3);
                string dynamicValueType = dynamic[0].ToUpperInvariant();

                if (dynamic.Length == 3){
                    switch(dynamicValueType){
                        case "IF": return new Condition(indexStart, indexEnd, dynamic[1], dynamic[2]);
                        case "FOR": return new Cycle(indexStart, indexEnd, dynamic[1], dynamic[2]);
                        case "VAR": return new Variable(indexStart, indexEnd, dynamic[1], dynamic[2]);
                        default: return null;
                    }
                }
                else if (dynamic.Length == 2){
                    switch(dynamicValueType){
                        case "VAR": return new Variable(indexStart, indexEnd, dynamic[1]);
                        default: return null;
                    }
                }
                else return null;
            }

            return new Template(indexStart, indexEnd, declaration);
        }

        private readonly int index, length;

        private TemplateToken(int index, int length){
            this.index = index;
            this.length = length;
        }

        public int ReplaceToken(int currentOffset, TemplateList list, Variables variables, ref string text){
            int prevLength = text.Length;
            text = text.Remove(index+currentOffset, length).Insert(index+currentOffset, GetTokenContents(list, variables));
            return currentOffset+text.Length-prevLength;
        }

        protected abstract string GetTokenContents(TemplateList list, Variables variables);

        public class Template : TemplateToken{
            protected readonly string templateName;

            public Template(int index, int length, string templateName) : base(index, length){
                this.templateName = templateName;
            }

            protected override string GetTokenContents(TemplateList list, Variables variables){
                return list.ProcessTemplate(templateName, variables);
            }
        }

        public class Variable : TemplateToken{
            private readonly string variableName;
            private readonly string defaultValue;

            public Variable(int index, int length, string variableName, string defaultValue = "") : base(index, length){
                this.variableName = variableName;
                this.defaultValue = defaultValue;
            }

            protected override string GetTokenContents(TemplateList list, Variables variables){
                return variables.GetVariable(variableName, defaultValue);
            }
        }

        public class Condition : Template{
            private readonly string conditionFlag;

            public Condition(int index, int length, string conditionFlag, string templateName) : base(index, length, templateName){
                this.conditionFlag = conditionFlag;
            }

            protected override string GetTokenContents(TemplateList list, Variables variables){
                return variables.CheckFlag(conditionFlag) ? list.ProcessTemplate(templateName, variables) : "";
            }
        }

        public class Cycle : Template{
            private readonly string cycleVariable;

            public Cycle(int index, int length, string cycleVariable, string templateName) : base(index, length, templateName){
                this.cycleVariable = cycleVariable;
            }

            protected override string GetTokenContents(TemplateList list, Variables variables){
                StringBuilder build = new StringBuilder();

                foreach(Variables entry in variables.GetArray(cycleVariable)){
                    build.Append(list.ProcessTemplate(templateName, entry));
                }

                return build.ToString();
            }
        }
    }
}
