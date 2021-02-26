using System;
using System.Collections.Generic;
using System.Text;

namespace RestauranteApp.Views
{
    class ViewPrinter
    {
        public static void Println(string text)
        {
            Console.WriteLine(text);
        }
        public static void Println(string text, ConsoleColor textColor)
        {
            ConsoleColor defaultTextColor = Console.ForegroundColor;

            Console.ForegroundColor = textColor;

            Console.WriteLine(text);

            Console.ForegroundColor = defaultTextColor;

        }
        public static void Println(string text, ConsoleColor textColor, ConsoleColor backgroundColor)
        {
            ConsoleColor defaultTextColor = Console.ForegroundColor;
            ConsoleColor defaultBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;

            Console.WriteLine(text);

            Console.ForegroundColor = defaultTextColor;
            Console.BackgroundColor = defaultBackgroundColor;
        }

        // --------------------------------------------------------------------------------------------------------

        public static void Print(string text)
        {
            Console.Write(text);
        }
        public static void Print(string text, ConsoleColor textColor)
        {
            ConsoleColor defaultTextColor = Console.ForegroundColor;

            Console.ForegroundColor = textColor;

            Console.Write(text);

            Console.ForegroundColor = defaultTextColor;

        }
        public static void Print(string text, ConsoleColor textColor, ConsoleColor backgroundColor)
        {
            ConsoleColor defaultTextColor = Console.ForegroundColor;
            ConsoleColor defaultBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;

            Console.Write(text);

            Console.ForegroundColor = defaultTextColor;
            Console.BackgroundColor = defaultBackgroundColor;
        }
    }
}
