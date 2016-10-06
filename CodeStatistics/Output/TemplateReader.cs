using CodeStatistics.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeStatistics.Output{
    class TemplateReader{
        private readonly string mainFile;
        private readonly string sourcePath;

        public TemplateReader(string mainFile){
            this.mainFile = mainFile;
            this.sourcePath = Path.GetDirectoryName(mainFile);
        }

        public IList<string> ReadLines(){
            List<string> lines = new List<string>();

            if (!File.Exists(mainFile)){
                throw new TemplateException(Lang.Get["TemplateErrorMainFileMissing", mainFile]);
            }

            ReadFileToList(mainFile, lines);
            return lines;
        }
        
        private void ReadFileToList(string file, ICollection<string> lines){ // TODO move to Lang
            string contents;

            try{
                contents = File.ReadAllText(file, Encoding.UTF8);
            }catch(Exception e){
                throw new TemplateException(Lang.Get["TemplateErrorFileRead", file], e);
            }

            string[] contentLines = contents.Split('\n', '\r');

            if (contentLines.Length == 0){
                throw new TemplateException(Lang.Get["TemplateErrorFileEmpty", file]);
            }

            foreach(string line in contentLines){
                TemplateDeclaration declaration;

                if (TemplateDeclaration.TryReadLine(line, out declaration)){
                    if (declaration.DefinesTemplate){
                        break;
                    }
                }
                else if (line.Trim().Length > 0){
                    throw new TemplateException(Lang.Get["TemplateErrorFileMissingDeclaration", file]);
                }
            }

            foreach(string line in contentLines){
                TemplateDeclaration declaration;

                if (TemplateDeclaration.TryReadLine(line, out declaration)){
                    if (declaration.Type == TemplateDeclaration.TemplateDeclarationType.Include){
                        string includedFile = Path.Combine(sourcePath, declaration.Name);

                        if (!IsFilePathValid(includedFile)){
                            throw new TemplateException(Lang.Get["TemplateErrorIncludedFileWrongPath", declaration.Name]);
                        }
                        else if (!File.Exists(includedFile)){
                            throw new TemplateException(Lang.Get["TemplateErrorIncludedFileMissing", includedFile]);
                        }
                        else{
                            ReadFileToList(includedFile, lines);
                        }

                        continue;
                    }
                }

                lines.Add(line);
            }
        }

        private bool IsFilePathValid(string path){
            return sourcePath.Equals(Path.GetDirectoryName(path), StringComparison.OrdinalIgnoreCase);
        }
    }
}
