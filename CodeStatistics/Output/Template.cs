using System.Collections.Generic;
using CodeStatisticsCore.Handling;

namespace CodeStatistics.Output{
    abstract class Template{
        private readonly string text;

        private Template(string text){
            this.text = text;
        }

        public abstract string Process(TemplateList list, Variables variables);

        public class Literal : Template{
            public Literal(string text) : base(text){}

            public override string Process(TemplateList list, Variables variables){
                return text;
            }
        }

        public class Dynamic : Template{
            private readonly List<TemplateToken> tokens = new List<TemplateToken>();

            public Dynamic(string text) : base(text){
                tokens.AddRange(TemplateToken.FindTokens(text));
            }

            public override string Process(TemplateList list, Variables variables){
                string processedText = text;
                int offset = 0;

                foreach(TemplateToken token in tokens){
                    offset = token.ReplaceToken(offset, list, variables, ref processedText);
                }

                return processedText;
            }
        }
    }
}
