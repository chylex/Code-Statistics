using CodeStatistics.Handling.General;
using CodeStatistics.Input;

namespace CodeStatistics.Handling.Languages{
    abstract class AbstractLanguageFileHandler : AbstractFileHandler{
        protected abstract string Key { get; }

        public override void Process(File file, Variables.Root variables){
            base.Process(file,variables);
            variables.AddFlag(Key);
            variables.Increment("fileTypeCode");

            ProcessFileContents(file,variables);
        }

        protected virtual void ProcessFileContents(File file, Variables.Root variables){
            string[] contents = file.Contents;

            int lineCount = 0, charCount = 0;

            foreach(string line in contents){
                if (!line.Trim().Equals("{")){
                    ++lineCount;
                }

                charCount += line.Length;
            }

            variables.Increment(Key+"LinesTotal",lineCount);
            variables.Increment(Key+"CharsTotal",charCount);
        }
    }
}
