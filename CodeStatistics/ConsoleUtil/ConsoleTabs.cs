using System;
using System.Collections.Generic;

namespace CodeStatistics.ConsoleUtil{
    class ConsoleTabs<T>{
        private static readonly ConsoleWrapper console = ConsoleWrapper.console;

        public delegate bool SelectEventHandler(T sender);

        public event SelectEventHandler Select;

        private readonly int y;
        private readonly bool enterToSelect;
        private List<Tab> tabs = new List<Tab>();
        private int selectedIndex = 0;

        public ConsoleTabs(int y, bool enterToSelect){
            this.y = y;
            this.enterToSelect = enterToSelect;
        }

        public void AddTab(string title, T handler){
            tabs.Add(new Tab(title,handler));
        }

        public void Render(){
            console.ClearLine(y);
            
            int width = 0, x = 0;
            for(int tab = 0; tab < tabs.Count; tab++)width += GetTabSize(tab);

            console.MoveToCenter(width,y);
            x = console.CursorX;

            for(int tab = 0; tab < tabs.Count; tab++){
                RenderTab(x,tab);
                x += GetTabSize(tab);
            }
        }

        private int GetTabSize(int index){
            return 3+tabs[index].Title.Length;
        }

        private void RenderTab(int x, int index){
            console.MoveTo(x,y);
            console.ResetForeground();
            console.Write("[");
            console.SetForeground(selectedIndex == index ? ConsoleColor.Yellow : ConsoleColor.Gray);
            console.Write(tabs[index].Title);
            console.ResetForeground();
            console.Write("]");
        }

        public T HandleInput(){
            if (tabs.Count == 0)return default(T);

            Console.CursorVisible = false;

            while(true){
                ConsoleKeyInfo info = Console.ReadKey(true);

                if (info.Key == ConsoleKey.LeftArrow){
                    if (--selectedIndex < 0)selectedIndex = tabs.Count-1;
                }
                else if (info.Key == ConsoleKey.RightArrow){
                    if (++selectedIndex >= tabs.Count)selectedIndex = 0;
                }
                else if (!(enterToSelect && info.Key == ConsoleKey.Enter))continue;

                Render();
                if ((enterToSelect == (info.Key == ConsoleKey.Enter)) && Select(tabs[selectedIndex].Object))break;
            }

            Console.CursorVisible = true;
            return tabs[selectedIndex].Object;
        }

        private struct Tab{
            public string Title;
            public T Object;

            public Tab(string title, T obj){
                this.Title = title;
                this.Object = obj;
            }
        }
    }
}
