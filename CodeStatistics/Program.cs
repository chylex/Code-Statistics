using CodeStatistics.ConsoleUtil;
using CodeStatistics.Handlers;
using CodeStatistics.Input;
using CodeStatistics.Input.Methods;
using System;
using System.Collections.Generic;

namespace CodeStatistics{
    static class Program{
        private const int TargetConsoleWidth = 140;
        private const int TargetConsoleHeight = 40;

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
            console.WriteCenter(centerY-2,"Choose your action or project selection method:");
            console.SetForeground(ConsoleColor.DarkGray);
            console.WriteCenter(centerY+4,"More ways to import projects (such as GitHub) will be added later.");

            ConsoleTabs<IProjectInputMethod> inputTabs = new ConsoleTabs<IProjectInputMethod>(centerY,true);
            inputTabs.AddTab("Folder",new MultiFolderDialog());
            inputTabs.AddTab("View Source",new OpenWebsite("https://github.com/chylex/Code-Statistics"));
            inputTabs.AddTab("Quit",new QuitProgram());

            inputTabs.Select += inputMethod => true; // breaks out
            inputTabs.Render();

            IProjectInputMethod selectedInputMethod;
            string[] rootFiles;
            
            while(true){
                selectedInputMethod = inputTabs.HandleInput();
                rootFiles = selectedInputMethod.Run(new string[0]);
                if (rootFiles != null)break;
            }

            // File search
            if (rootFiles.Length == 0)return;

            FileSearch search = new FileSearch(rootFiles);

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
            console.SetForeground(ConsoleColor.DarkYellow);
            console.WriteCenter(centerY,new string('▒',50));

            analyzer.Update += (percentage, handledFiles, totalFiles) => {
                if (handledFiles == totalFiles){
                    console.ClearLine(centerY-2);
                    console.SetForeground(ConsoleColor.White);
                    console.WriteCenter(centerY-2,"Generating tabs...");
                }
                else{
                    console.MoveToCenter(50,centerY);
                    console.Write(new string('▒',(int)Math.Floor(percentage*50F)),ConsoleColor.Yellow);

                    console.SetForeground(ConsoleColor.Gray);
                    console.WriteCenter(centerY+2,handledFiles+" / "+totalFiles);
                }
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
                for(int py = y+3; py < height-1; py++)console.ClearLine(py);
                tab.RenderInfo(console,y+3);
                return false;
            };

            tabs.Render();
            tabs.HandleInput(true);

            // Pause
            Console.ReadKey();
        }
    }
}
