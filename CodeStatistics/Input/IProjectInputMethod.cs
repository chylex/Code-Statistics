namespace CodeStatistics.Input{
    interface IProjectInputMethod{
        /// <summary>
        /// Return list of found files. If an empty list is returned, the program is shut down. If null is returned, the program keeps waiting in the menu.
        /// </summary>
        string[] Run(string[] args);
    }
}
