using System;

namespace CodeStatistics.ConsoleUtil{
    class ConsoleWrapper{
        public static readonly ConsoleWrapper console = new ConsoleWrapper();

        public int Width{ get { return Console.BufferWidth; } }
        public int Height{ get { return Console.BufferHeight; } }
        public int CursorX{ get { return Console.CursorLeft; } }
        public int CursorY{ get { return Console.CursorTop; } }

        private ConsoleColor Foreground, Background;

        private ConsoleWrapper(){
            Foreground = Console.ForegroundColor;
            Background = Console.BackgroundColor;
        }

        public void SetForeground(ConsoleColor color){
            Foreground = color;
        }

        public void SetBackground(ConsoleColor color){
            Background = color;
        }

        public void ResetForeground(){
            Foreground = ConsoleColor.Gray;
        }

        public void ResetBackground(){
            Background = ConsoleColor.Black;
        }

        public void Clear(){
            Console.Clear();
        }

        public void Clear(int x, int y, int width, int height){
            Console.SetCursorPosition(x,y);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            string line = new string(' ',width);
            
            for(int ver = 0; ver < height; ver++){
                Console.Write(line);
                Console.SetCursorPosition(x,y+ver);
            }
        }

        public void ClearLine(int y){
            Clear(0,y,Width,1);
        }

        public void MoveTo(int x, int y){
            Console.SetCursorPosition(x,y);
        }

        public void MoveToCenter(int width, int y){
            Console.SetCursorPosition(this.Width/2-width/2,y);
        }

        public void Write(int x, int y, string text){
            Console.SetCursorPosition(x,y);
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
            Console.Write(text);
        }

        public void Write(string text){
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
            Console.Write(text);
        }

        public void WriteCenter(int y, string text){
            MoveToCenter(text.Length,y);
            Write(text);
        }
    }
}
