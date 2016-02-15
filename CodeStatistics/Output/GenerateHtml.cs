using System;
using System.Collections.Generic;
using CodeStatistics.Handling;
using System.IO;
using System.Text;

namespace CodeStatistics.Output{
    class GenerateHtml{
        private readonly TemplateList templates;
        private readonly Variables variables;

        public GenerateHtml(IList<string> templateLines, Variables variables){
            this.templates = new TemplateList(templateLines);
            this.variables = variables;
        }

        public bool ToFile(string file){
            string contents = templates.ProcessTemplate("html",variables);

            try{
                File.WriteAllText(file,contents,Encoding.UTF8);
                return File.Exists(file);
            }catch(Exception){
                return false;
            }
        }
    }
}
