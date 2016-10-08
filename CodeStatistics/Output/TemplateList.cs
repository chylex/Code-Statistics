using System;
using System.Collections.Generic;
using CodeStatistics.Data;
using CodeStatisticsCore.Handling;

namespace CodeStatistics.Output{
    class TemplateList{
        private readonly Dictionary<string, Template> templates = new Dictionary<string, Template>();

        public void AddTemplate(TemplateDeclaration declaration, string text){
            AddTemplate(declaration.Name, declaration.CreateTemplate(text));
        }

        public void AddTemplate(string templateName, Template template){
            try{
                templates.Add(templateName, template);
            }catch(ArgumentException e){
                throw new TemplateException(Lang.Get["TemplateErrorDuplicate", templateName], e);
            }
        }

        public string ProcessTemplate(string templateName, Variables variables){
            Template template;

            if (templates.TryGetValue(templateName, out template)){
                return template.Process(this, variables);
            }
            else{
                throw new TemplateException(Lang.Get["TemplateErrorNotFound", templateName]);
            }
        }
    }
}
