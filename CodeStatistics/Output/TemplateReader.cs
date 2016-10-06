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
                throw new FileNotFoundException("Main template file not found.", mainFile);
            }

            ReadFileToList(mainFile, lines);
            return lines;
        }

        private void ReadFileToList(string file, ICollection<string> lines){
            string contents;

            try{
                contents = File.ReadAllText(file, Encoding.UTF8);
            }catch(Exception e){
                throw new TemplateException("Error reading a template file: "+file, e);
            }

            string[] contentLines = contents.Split('\n', '\r');

            if (contentLines.Length == 0){
                throw new TemplateException("Template file is empty: "+file);
            }

            foreach(string line in contentLines){
                TemplateDeclaration declaration;

                if (TemplateDeclaration.TryReadLine(line, out declaration)){
                    if (declaration.DefinesTemplate){
                        break;
                    }
                }
                else if (line.Trim().Length > 0){
                    throw new TemplateException("Template file does not begin with a template declaration: "+file);
                }
            }

            foreach(string line in contentLines){
                TemplateDeclaration declaration;

                if (TemplateDeclaration.TryReadLine(line, out declaration)){
                    if (declaration.Type == TemplateDeclaration.TemplateDeclarationType.Include){
                        string includedFile = Path.Combine(sourcePath, declaration.Name);

                        if (!IsFilePathValid(includedFile)){
                            throw new TemplateException("Included template file must be in the same directory: "+declaration.Name);
                        }
                        else if (!File.Exists(includedFile)){
                            throw new TemplateException("Included template file not found: "+includedFile);
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
