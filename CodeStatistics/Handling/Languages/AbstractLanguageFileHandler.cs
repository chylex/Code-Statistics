using CodeStatistics.Handling.General;
using CodeStatistics.Input;

namespace CodeStatistics.Handling.Languages{
    abstract class AbstractLanguageFileHandler : AbstractFileHandler{
        public override void Process(File file, Variables.Root variables){
            base.Process(file,variables);
            variables.Increment("fileTypeCode");
        }
    }
}
