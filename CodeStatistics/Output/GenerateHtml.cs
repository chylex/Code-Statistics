using System;
using System.Collections.Generic;
using CodeStatistics.Handling;
using System.IO;
using System.Text;

namespace CodeStatistics.Output{
    class GenerateHtml{
        public enum Result{
            Succeeded, TemplateError, IoError
        }

        private readonly TemplateList templates;
        private readonly Variables variables;

        public string LastError { get; private set; }

        public GenerateHtml(IList<string> templateLines, Variables variables){
            this.templates = new TemplateList(templateLines);
            this.variables = variables;
            this.LastError = string.Empty;
        }

        public Result ToFile(string file){
            string contents;

            try{
                contents = templates.ProcessTemplate("html",variables);
            }catch(Exception e){
                LastError = e.Message;
                return Result.TemplateError;
            }

            try{
                File.WriteAllText(file,contents,Encoding.UTF8);
                return File.Exists(file) ? Result.Succeeded : Result.IoError;
            }catch(Exception e){
                LastError = e.ToString();
                return Result.IoError;
            }
        }
    }
}
