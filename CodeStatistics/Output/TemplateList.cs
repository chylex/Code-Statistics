using CodeStatistics.Handling;
using System.Collections.Generic;
using CodeStatistics.Data;

namespace CodeStatistics.Output{
    class TemplateList{
        private readonly Dictionary<string,Template> templates = new Dictionary<string,Template>();

        public TemplateList(IList<string> lines){
            for(int lineIndex = 0; lineIndex < lines.Count; lineIndex++){
                string line = lines[lineIndex];
                TemplateDeclaration declaration;

                if (TemplateDeclaration.TryReadLine(line,out declaration)){
                    var templateLines = new List<string>();

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
            Template template;

            if (templates.TryGetValue(templateName,out template)){
                return template.Process(this,variables);
            }
            else{
                throw new TemplateException(Lang.Get["TemplateErrorNotFound",templateName]);
            }
        }
    }
}
