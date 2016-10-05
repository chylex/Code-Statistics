using System;
using System.Collections.Generic;
using System.Linq;
using CodeStatisticsCore.Collections;
using CodeStatisticsCore.Handling.Utils;
using CodeStatisticsCore.Input;

namespace CodeStatisticsCore.Handling.Files{
    public abstract class AbstractLanguageFileHandler : AbstractFileHandler{
        protected abstract string Key { get; }

        private readonly object stateOwner = new object();

        public override void SetupProject(Variables.Root variables){
            base.SetupProject(variables);
            variables.AddFlag(Key);
            variables.AddStateObject(stateOwner, new State());
        }

        public override void Process(File file, Variables.Root variables){
            base.Process(file, variables);

            variables.Increment("fileTypeCode");
            variables.Increment(Key+"CodeFiles");

            ProcessFileContents(file, variables);
        }

        protected virtual void ProcessFileContents(File file, Variables.Root variables){
            string[] contents = file.Contents.Split('\n');

            int lineCount = 0, charCount = 0, maxCharsPerLine = 0;

            foreach(string line in contents){
                if (!line.Trim().Equals("{")){
                    ++lineCount;
                }

                if (line.Length > 0){
                    int realLength = ParseUtils.CountCharacters(line);

                    charCount += realLength;
                    maxCharsPerLine = Math.Max(maxCharsPerLine, realLength);
                }
            }

            variables.Increment(Key+"LinesTotal", lineCount);
            variables.Increment(Key+"CharsTotal", charCount);
            variables.Maximum(Key+"LinesMax", lineCount);
            variables.Maximum(Key+"CharsMax", charCount);
            variables.Maximum(Key+"CharsPerLineMax", maxCharsPerLine);

            State state = variables.GetStateObject<State>(stateOwner);

            FileIntValue fileLines = new FileIntValue(file, lineCount);
            state.MaxLines.Add(fileLines);
            state.MinLines.Add(fileLines);

            FileIntValue fileChars = new FileIntValue(file, charCount);
            state.MaxChars.Add(fileChars);
            state.MinChars.Add(fileChars);
        }

        public override void FinalizeProject(Variables.Root variables){
            base.FinalizeProject(variables);

            variables.Average(Key+"LinesAvg", Key+"LinesTotal", Key+"CodeFiles");
            variables.Average(Key+"CharsAvg", Key+"CharsTotal", Key+"CodeFiles");
            variables.Average(Key+"CharsPerLineAvg", Key+"CharsTotal", Key+"LinesTotal");
            
            State state = variables.GetStateObject<State>(stateOwner);

            foreach(FileIntValue fi in state.MaxLines){
                variables.AddToArray(Key+"LinesTop", GetFileObject(fi, variables));
            }

            foreach(FileIntValue fi in state.MinLines.Reverse()){
                variables.AddToArray(Key+"LinesBottom", GetFileObject(fi, variables));
            }

            foreach(FileIntValue fi in state.MaxChars){
                variables.AddToArray(Key+"CharsTop", GetFileObject(fi, variables));
            }

            foreach(FileIntValue fi in state.MinChars.Reverse()){
                variables.AddToArray(Key+"CharsBottom", GetFileObject(fi, variables));
            }
        }

        protected abstract object GetFileObject(FileIntValue fi, Variables.Root variables);
        public abstract string PrepareFileContents(string contents);
        public abstract IEnumerable<Node> GenerateTreeViewData(Variables.Root variables, File file);

        private class State{
            public readonly TopElementList<FileIntValue> MaxLines = new TopElementList<FileIntValue>(8, FileIntValue.SortMax);
            public readonly TopElementList<FileIntValue> MinLines = new TopElementList<FileIntValue>(8, FileIntValue.SortMin);

            public readonly TopElementList<FileIntValue> MaxChars = new TopElementList<FileIntValue>(8, FileIntValue.SortMax);
            public readonly TopElementList<FileIntValue> MinChars = new TopElementList<FileIntValue>(8, FileIntValue.SortMin);
        }
    }
}
