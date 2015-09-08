using CodeStatistics.ConsoleUtil;
using CodeStatistics.Handlers;
using CodeStatistics.Input;
using System;
using System.Collections.Generic;

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

            Console.CursorVisible = false;
            HashSet<File> foundFiles = search.Search();

            // File parsing
            ProjectAnalyzer analyzer = new ProjectAnalyzer(foundFiles);

            console.Clear();
            console.SetForeground(ConsoleColor.White);
            console.WriteCenter(1,"Processing files...");

            analyzer.Update += (percentage, handledFiles, totalFiles) => {
                console.ClearLine(3);
                console.SetForeground(ConsoleColor.Yellow);
                console.MoveToCenter(50,3);
                console.Write(new string('▒',(int)Math.Floor(percentage*50F)));
                console.SetForeground(ConsoleColor.Gray);
                console.WriteCenter(5,handledFiles+" / "+totalFiles);
            };

            List<IHandlerTab> generatedTabs = analyzer.Run();

            // Tab generation
            const int y = 1;

            ConsoleTabs<IHandlerTab> tabs = new ConsoleTabs<IHandlerTab>(y,false);
            foreach(IHandlerTab tab in generatedTabs)tabs.AddTab(tab.GetName(),tab);

            tabs.Select += tab => {
                tab.RenderInfo(console,y+2);
                return false;
            };

            tabs.Render();
            tabs.HandleInput();

            // Pause
            Console.ReadKey();
        }
    }
}
