using CodeStatistics.Input;
using System;
using System.Globalization;
using System.IO;
using CodeStatistics.Properties;
using System.Text;
using CodeStatistics.Data;
using DirectoryIO = System.IO.Directory;
using FileIO = System.IO.File;
using CodeStatistics.Input.Methods;

namespace CodeStatistics{
    class ProgramConfiguration{
        public static bool Validate(ProgramArguments.Argument argument, Action<string> setError){
            switch(argument.Name){
                case "nogui":
                case "openbrowser":
                case "autoclose":
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

                    if (argument.Name == "in:folder"){
                        if (!DirectoryIO.Exists(argument.Value)){
                            setError(Lang.Get["ErrorInvalidArgsFileNotFound",argument.Value]);
                            return false;
                        }
                    }
                    else if (argument.Name == "in:archive"){
                        if (!FileIO.Exists(argument.Value)){
                            setError(Lang.Get["ErrorInvalidArgsFileNotFound",argument.Value]);
                            return false;
                        }
                    }
                    else if (argument.Name == "in:github"){
                        if (!GitHub.IsRepositoryValid(argument.Value)){
                            setError(Lang.Get["ErrorInvalidArgsGitHub",argument.Value]);
                            return false;
                        }
                    }

                    return true;
            }

            setError(Lang.Get["ErrorInvalidArgsUnknown",argument.Name]);
            return false;
        }

        private enum InputType{
            Dummy, Folder, Archive, GitHub
        }

        private readonly string outputFile, templateFile;
        private readonly InputType? inputType;
        private readonly string inputValue;

        public readonly bool AutoOpenBrowser;
        public readonly bool CloseOnFinish;

        public ProgramConfiguration(ProgramArguments args){
            outputFile = args.HasVariable("out") ? args.GetVariable("out") : null;
            templateFile = args.HasVariable("template") ? args.GetVariable("template") : null;

            AutoOpenBrowser = args.CheckFlag("openbrowser");
            CloseOnFinish = args.CheckFlag("autoclose");

            if (args.CheckFlag("in:dummy")){
                inputType = InputType.Dummy;
            }
            else if (args.HasVariable("in:folder")){
                inputType = InputType.Folder;
                inputValue = args.GetVariable("in:folder");
            }
            else if (args.HasVariable("in:archive")){
                inputType = InputType.Archive;
                inputValue = args.GetVariable("in:archive");
            }
            else if (args.HasVariable("in:github")){
                inputType = InputType.GitHub;
                inputValue = args.GetVariable("in:github");
            }
        }

        public IInputMethod GetImmediateInputMethod(){
            switch(inputType){
                case InputType.Dummy: return new DummyInputMethod();
                case InputType.Folder: return new FileSearch(new string[]{ inputValue });
                case InputType.Archive: return new ArchiveExtraction(inputValue,IOUtils.CreateTemporaryDirectory());
                case InputType.GitHub: return new GitHub(inputValue);
                default: return null;
            }
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
                    return FileIO.ReadAllText(templateFile,Encoding.UTF8);
                }catch(Exception){
                    return null;
                }
            }
        }
    }
}
