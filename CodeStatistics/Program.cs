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

            };

            inputTabs.Render();
            inputTabs.HandleInput();

            // Pause
            Console.ReadKey();
        }
    }
}
