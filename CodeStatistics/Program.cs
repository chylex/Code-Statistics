using CodeStatistics.ConsoleUtil;
using CodeStatistics.Handlers;
using CodeStatistics.Input;
using CodeStatistics.Input.Methods;
using System;
using System.Collections.Generic;

namespace CodeStatistics{
    class Program{
        private static readonly int TargetConsoleWidth = 120, TargetConsoleHeight = 40;

        [STAThread]
        static void Main(string[] args){
            int width = Math.Min(TargetConsoleWidth,Console.LargestWindowWidth);
            int height = Math.Min(TargetConsoleHeight,Console.LargestWindowHeight);
            int centerY = height/2-1;
            Console.SetWindowSize(width,height);
            Console.SetBufferSize(width,height);

            ConsoleWrapper console = ConsoleWrapper.console;

            // Project input method
            console.SetForeground(ConsoleColor.White);
            console.WriteCenter(centerY-1,"Choose your project selection method:");

            ConsoleTabs<IProjectInputMethod> inputTabs = new ConsoleTabs<IProjectInputMethod>(centerY+1,true);
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
            console.WriteCenter(centerY-1,"Searching for files...");
            console.SetForeground(ConsoleColor.Yellow);

            search.Refresh += fileCount => {
                console.WriteCenter(centerY+1,fileCount.ToString());
            };

            Console.CursorVisible = false;
            HashSet<File> foundFiles = search.Search();

            // File parsing
            ProjectAnalyzer analyzer = new ProjectAnalyzer(foundFiles);

            console.Clear();
            console.SetForeground(ConsoleColor.White);
            console.WriteCenter(centerY-2,"Processing files...");

            analyzer.Update += (percentage, handledFiles, totalFiles) => {
                console.ClearLine(centerY);
                console.SetForeground(ConsoleColor.Yellow);
                console.MoveToCenter(50,centerY);
                console.Write(new string('▒',(int)Math.Floor(percentage*50F)));
                console.SetForeground(ConsoleColor.Gray);
                console.WriteCenter(centerY+2,handledFiles+" / "+totalFiles);
            };

            List<IHandlerTab> generatedTabs = analyzer.Run();

            // Tab generation
            console.Clear();
            console.SetForeground(ConsoleColor.White);
            console.WriteCenter(1,"");

            const int y = 1;

            ConsoleTabs<IHandlerTab> tabs = new ConsoleTabs<IHandlerTab>(y,false);
            foreach(IHandlerTab tab in generatedTabs)tabs.AddTab(tab.GetName(),tab);

            tabs.Select += tab => {
                for(int py = y+2; py < height-1; py++)console.ClearLine(py);
                tab.RenderInfo(console,y+2);
                return false;
            };

            tabs.Render();
            tabs.HandleInput(true);

            // Pause
            Console.ReadKey();
        }
    }
}
