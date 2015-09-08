using CodeStatistics.ConsoleUtil;
using CodeStatistics.Input;
using System;

namespace CodeStatistics{
    class Program{
        private static readonly int ConsoleWidth = 120, ConsoleHeight = 40;

        [STAThread]
        static void Main(string[] args){
            int width = Math.Min(ConsoleWidth,Console.LargestWindowWidth);
            int height = Math.Min(ConsoleHeight,Console.LargestWindowHeight);
            Console.SetWindowSize(width,height);
            Console.SetBufferSize(width,height);

            ConsoleWrapper console = ConsoleWrapper.console;

            // Project input method
            console.SetForeground(ConsoleColor.White);
            console.WriteCenter(1,"Choose your project selection method:");

            ConsoleTabs<IProjectInputMethod> inputTabs = new ConsoleTabs<IProjectInputMethod>(3,true);
            inputTabs.AddTab("Folder",new MultiFolderDialog());
            inputTabs.AddTab("GitHub",new MultiFolderDialog());
            inputTabs.AddTab("Magic",new MultiFolderDialog());

            inputTabs.Select += inputMethod => true; // breaks out
            inputTabs.Render();
            IProjectInputMethod selectedInputMethod = inputTabs.HandleInput();

            // File search
            string[] files = selectedInputMethod.Run(new string[0]);
            if (files.Length == 0)return;

            FileSearch search = new FileSearch(files);

            console.Clear();
            console.SetForeground(ConsoleColor.White);
            console.WriteCenter(1,"Searching for files...");
            console.SetForeground(ConsoleColor.Yellow);

            search.Refresh += fileCount => {
                console.WriteCenter(3,fileCount.ToString());
            };

            search.Finish += foundFiles => {
                
            };

            Console.CursorVisible = false;
            search.Search();
            Console.CursorVisible = true;

            // Pause
            Console.ReadKey();
        }
    }
}
