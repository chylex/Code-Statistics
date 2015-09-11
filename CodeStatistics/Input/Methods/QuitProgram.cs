namespace CodeStatistics.Input.Methods{
    class QuitProgram : IProjectInputMethod{
        public string[] Run(string[] args){
            return new string[0]; // automatically quits when an empty array is returned
        }
    }
}
