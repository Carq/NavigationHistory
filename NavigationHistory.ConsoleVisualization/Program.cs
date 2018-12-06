using NavigationHistory.ConsoleVisualization.NavigationPages;
using System;

namespace NavigationHistory.ConsoleVisualization
{
    class Program
    {
        private static Lib.NavigationHistory<BasePage> NavigationHistory = new Lib.NavigationHistory<BasePage>();

        static void Main(string[] args)
        {
            DisplayInstruction();

            ConsoleKey pressedKey = ConsoleKey.NoName;
            do
            {
                pressedKey = Console.ReadKey().Key;
                Console.Write('\r');

                switch (pressedKey)
                {
                    case ConsoleKey.D0:
                        NavigationHistory.Record(new HomePage());
                        break;
                    case ConsoleKey.D1:
                        NavigationHistory.Record(new Page1());
                        break;
                    case ConsoleKey.D2:
                        NavigationHistory.Record(new Page2());
                        break;
                    case ConsoleKey.D3:
                        NavigationHistory.Record(new Page3());
                        break;
                    case ConsoleKey.B:
                        if (!NavigationHistory.CanMoveBack())
                        {
                            continue;
                        }

                        NavigationHistory.Back();
                        break;
                    case ConsoleKey.F:
                        if (!NavigationHistory.CanMoveForward())
                        {
                            continue;
                        }

                        NavigationHistory.Forward();
                        break;
                }

                DisplayCurrentPage();
            }
            while (pressedKey != ConsoleKey.Escape && pressedKey != ConsoleKey.Enter);
            
        }

        private static void DisplayCurrentPage()
        {
            if (NavigationHistory.CurrentItem == null)
            {
                Console.WriteLine();
                Console.WriteLine("Empty page");
                return;
            }

            Console.WriteLine();
            Console.Write($"You are on");
            Console.ForegroundColor = NavigationHistory.CurrentItem.Color;
            Console.Write($" {NavigationHistory.CurrentItem.PageNumber} ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"page");
            Console.WriteLine();
        }

        private static void DisplayInstruction()
        {
            Console.WriteLine("0, 1, 2, 3 - Pick page");
            Console.WriteLine("b - Back");
            Console.WriteLine("f - Forward");
            Console.WriteLine("Enter, ESC - Exit");
        }
    }
}
