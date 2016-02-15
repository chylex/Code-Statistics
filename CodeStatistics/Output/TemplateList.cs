using CodeStatistics.Handling;
using System.Collections.Generic;

namespace CodeStatistics.Output{
    class TemplateList{
        private readonly Dictionary<string,Template> templates = new Dictionary<string,Template>();

        public TemplateList(IList<string> lines){
            for(int lineIndex = 0; lineIndex < lines.Count; lineIndex++){
                string line = lines[lineIndex];
                TemplateDeclaration declaration;

                if (TemplateDeclaration.TryReadLine(line,out declaration)){
                    List<string> templateLines = new List<string>();

                    while(++lineIndex < lines.Count){
                        string templateLine = lines[lineIndex];
                        if (templateLine.Length == 0)continue;

                        if (TemplateDeclaration.IsDeclaration(templateLine)){
                            --lineIndex;
                            break;
                        }

                        templateLines.Add(templateLine);
                    }

                    templates[declaration.Name] = declaration.CreateTemplate(string.Join("\r\n",templateLines));
                }
            }
        }

        public string ProcessTemplate(string templateName, Variables variables){
            return templates[templateName].Process(this,variables); // throws KeyNotFoundException if the template is missing
        }
    }
}
