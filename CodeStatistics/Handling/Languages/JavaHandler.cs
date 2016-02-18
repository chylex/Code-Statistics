using CodeStatistics.Input;

namespace CodeStatistics.Handling.Languages {
    class JavaHandler : AbstractLanguageFileHandler{
        public override int Weight{
            get { return 50; }
        }

        protected override string Key{
            get { return "java"; }
        }

        public override void SetupProject(Variables.Root variables){
            base.SetupProject(variables);
        }

        public override void Process(File file, Variables.Root variables){
            base.Process(file,variables);

            // TODO
        }
    }
}
