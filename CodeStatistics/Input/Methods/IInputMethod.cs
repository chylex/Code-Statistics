namespace CodeStatistics.Input.Methods{
    interface IInputMethod{
        void BeginProcess(FileSearch.OnInputReady onReady);
    }
}
