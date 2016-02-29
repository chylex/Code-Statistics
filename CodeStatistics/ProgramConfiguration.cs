using CodeStatistics.Input;
using System;
using System.IO;

namespace CodeStatistics{
    class ProgramConfiguration{
        private readonly string outputFile;

        public ProgramConfiguration(ProgramArguments args){
            outputFile = args.HasVariable("out") ? args.GetVariable("out") : null;
        }

        public string GetOutputFilePath(){
            if (outputFile == null){
                return Path.Combine(IOUtils.CreateTemporaryDirectory(),"output.html");
            }
            else{
                return Path.IsPathRooted(outputFile) ? outputFile : Path.Combine(Environment.CurrentDirectory,outputFile);
            }
        }
    }
}
