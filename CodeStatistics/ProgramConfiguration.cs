﻿using CodeStatistics.Input;
using System;
using System.IO;
using CodeStatistics.Data;
using DirectoryIO = System.IO.Directory;
using FileIO = System.IO.File;
using CodeStatistics.Input.Methods;
using CodeStatisticsCore.Input;

namespace CodeStatistics{
    class ProgramConfiguration{
        public static bool Validate(ProgramArguments.Argument argument, Action<string> setError){
            switch(argument.Name){
                case "nogui":
                case "openbrowser":
                case "autoclose":
                case "debug":
                case "in:dummy":
                    return true;

                case "template":
                case "template:debug":
                case "in:folder":
                case "in:archive":
                case "in:github":
                case "out":
                    if (argument.IsFlag){
                        setError(Lang.Get["ErrorInvalidArgsShouldBeVariable", argument.Name]);
                        return false;
                    }

                    if (argument.Name == "in:folder"){
                        if (!DirectoryIO.Exists(argument.Value)){
                            setError(Lang.Get["ErrorInvalidArgsFileNotFound", argument.Value]);
                            return false;
                        }
                    }
                    else if (argument.Name == "in:archive"){
                        if (!FileIO.Exists(argument.Value)){
                            setError(Lang.Get["ErrorInvalidArgsFileNotFound", argument.Value]);
                            return false;
                        }
                    }
                    else if (argument.Name == "in:github"){
                        if (!GitHub.IsRepositoryValid(argument.Value)){
                            setError(Lang.Get["ErrorInvalidArgsGitHub", argument.Value]);
                            return false;
                        }
                    }

                    return true;
            }

            setError(Lang.Get["ErrorInvalidArgsUnknown", argument.Name]);
            return false;
        }

        private enum InputType{
            Dummy, Folder, Archive, GitHub
        }

        private readonly string outputFile, templateFile;
        private readonly InputType? inputType;
        private readonly string inputValue;

        public readonly bool NoGui;
        public readonly bool AutoOpenBrowser;
        public readonly bool CloseOnFinish;
        public readonly bool IsDebuggingProject;
        public readonly bool IsDebuggingTemplate;

        public ProgramConfiguration(ProgramArguments args){
            outputFile = args.HasVariable("out") ? args.GetVariable("out") : null;
            templateFile = args.HasVariable("template") ? args.GetVariable("template") : args.HasVariable("template:debug") ? args.GetVariable("template:debug") : null;

            NoGui = args.CheckFlag("nogui");
            AutoOpenBrowser = args.CheckFlag("openbrowser");
            CloseOnFinish = args.CheckFlag("autoclose");
            IsDebuggingProject = args.HasVariable("debug");
            IsDebuggingTemplate = args.HasVariable("template:debug");

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
                case InputType.Archive: return new ArchiveExtraction(inputValue, IOUtils.CreateTemporaryDirectory());
                case InputType.GitHub: return new GitHub(inputValue);
                default: return null;
            }
        }

        public string GetOutputFilePath(){
            if (outputFile == null){
                return Path.Combine(IOUtils.CreateTemporaryDirectory(), "output.html");
            }
            else{
                return Path.IsPathRooted(outputFile) ? outputFile : Path.Combine(Environment.CurrentDirectory, outputFile);
            }
        }

        public string GetCustomTemplateFilePath(){
            return Path.IsPathRooted(templateFile) ? templateFile : Path.Combine(Environment.CurrentDirectory, templateFile);
        }

        public string GetTemplateFilePath(){
            if (templateFile == null){
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template", "template.html");
            }
            else{
                return templateFile;
            }
        }
    }
}
