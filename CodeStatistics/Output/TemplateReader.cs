using CodeStatistics.Data;
using System;
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

        public TemplateList ReadTemplates(){
            TemplateList templates = new TemplateList();

            if (!File.Exists(mainFile)){
                throw new TemplateException(Lang.Get["TemplateErrorMainFileMissing", mainFile]);
            }

            ReadFileToList(mainFile, templates);
            return templates;
        }
        
        private void ReadFileToList(string file, TemplateList list){
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

            for(int lineIndex = 0; lineIndex < contentLines.Length; lineIndex++){
                string line = contentLines[lineIndex];

                TemplateDeclaration declaration;

                if (TemplateDeclaration.TryReadLine(line, out declaration)){
                    if (declaration.DefinesTemplate){
                        StringBuilder build = new StringBuilder();

                        while(++lineIndex < contentLines.Length){
                            string templateLine = contentLines[lineIndex];

                            if (TemplateDeclaration.IsValidDeclaration(templateLine)){
                                --lineIndex;
                                break;
                            }

                            build.Append(templateLine).Append("\r\n");
                        }

                        list.AddTemplate(declaration, build.ToString());
                    }
                    else if (declaration.Type == TemplateDeclaration.TemplateDeclarationType.Include){
                        string includedFile = Path.Combine(sourcePath, declaration.Name);

                        if (!IsFilePathValid(includedFile)){
                            throw new TemplateException(Lang.Get["TemplateErrorIncludedFileWrongPath", declaration.Name]);
                        }
                        else if (!File.Exists(includedFile)){
                            throw new TemplateException(Lang.Get["TemplateErrorIncludedFileMissing", includedFile]);
                        }
                        else{
                            ReadFileToList(includedFile, list);
                        }
                    }
                }
                else if (line.Trim().Length > 0){
                    throw new TemplateException(Lang.Get["TemplateErrorFileMissingDeclaration", file, lineIndex]);
                }
            }
        }

        private bool IsFilePathValid(string path){
            return sourcePath.Equals(Path.GetDirectoryName(path), StringComparison.OrdinalIgnoreCase);
        }
    }
}
