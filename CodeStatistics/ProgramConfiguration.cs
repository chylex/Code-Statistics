using CodeStatistics.Input;
using System;
using System.Globalization;
using System.IO;
using CodeStatistics.Properties;
using System.Text;
using CodeStatistics.Data;

namespace CodeStatistics{
    class ProgramConfiguration{
        public static bool Validate(ProgramArguments.Argument argument, Action<string> setError){
            switch(argument.Name){
                case "nogui":
                case "openbrowser":
                case "in:dummy":
                    return true;

                case "template":
                case "template:debug":
                case "in:folder":
                case "in:archive":
                case "in:github":
                case "out":
                    if (argument.IsFlag){
                        setError(Lang.Get["ErrorInvalidArgsShouldBeVariable",argument.Name]);
                        return false;
                    }

                    if (argument.Name == "in:github"){
                        // TODO
                    }

                    return true;
            }

            setError(Lang.Get["ErrorInvalidArgsUnknown",argument.Name]);
            return false;
        }

        private readonly string outputFile, templateFile;

        public ProgramConfiguration(ProgramArguments args){
            outputFile = args.HasVariable("out") ? args.GetVariable("out") : null;
            templateFile = args.HasVariable("template") ? args.GetVariable("template") : null;
        }

        public string GetOutputFilePath(){
            if (outputFile == null){
                return Path.Combine(IOUtils.CreateTemporaryDirectory(),"output.html");
            }
            else{
                return Path.IsPathRooted(outputFile) ? outputFile : Path.Combine(Environment.CurrentDirectory,outputFile);
            }
        }

        public string GetTemplateContents(){
            if (templateFile == null){
                return Resources.ResourceManager.GetString("template",CultureInfo.InvariantCulture); // may be null
            }
            else{
                try{
                    return System.IO.File.ReadAllText(templateFile,Encoding.UTF8);
                }catch(Exception){
                    return null;
                }
            }
        }
    }
}
