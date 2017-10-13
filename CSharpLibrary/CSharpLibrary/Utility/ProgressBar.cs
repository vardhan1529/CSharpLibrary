using System;

namespace CSharpLibrary.Utility
{
    class ProgressBar
    {
        static void ProgressCheck()
        {
            Console.CursorVisible = false;
            Console.Write("[ ");
            Console.CursorLeft = 102;
            Console.Write(" ]");
            for (var i = 1; i <= 100; i++)
            {
                System.Threading.Thread.Sleep(20);
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.CursorLeft = i + 1;
                Console.Write(" ");
                Console.CursorLeft = 106;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(i + "%");
            }

            Console.CursorVisible = true;
            Console.WriteLine();
        }
    }
}
