using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using Relaciones_entre_clases.Cars;

namespace Relaciones_entre_clases.Sistem
{
    public static class Menu
    {
        public static string DisplayMenuOptions(List<string> options, string title)
        {
            Console.Beep();
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(title);
            Console.ResetColor();
            (int left, int top) = Console.GetCursorPosition();

            List<string> optionsList = options;

            var selection = 0;
            var color = "✅ ";
            ConsoleKeyInfo key;
            bool isSelected = false;

            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);

                foreach (var op in options)
                {
                    Console.WriteLine($"{(selection == options.IndexOf(op) ? color : "   ")}{op}\u001b[0m");
                }

                key = Console.ReadKey(false);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selection = selection == 0 ? options.Count - 1 : selection - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        selection = selection == options.Count - 1 ? 0 : selection + 1;
                        break;

                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
            }

            Console.WriteLine($"\n{color}You selected {options[selection]}");
            return options[selection];

        }

        public static T DisplayMenuOptions<T>(Dictionary<string, T> options, string title)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(title);
            Console.ResetColor();
            (int left, int top) = Console.GetCursorPosition();

            var selection = 0;
            var color = "✅ \u001b[32m";
            ConsoleKeyInfo key;
            bool isSelected = false;

            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);

                foreach (var op in options)
                {
                    Console.WriteLine($"{(selection == options.Keys.ToList().IndexOf(op.Key) ? color : "   ")}{op.Key}\u001b[0m");
                }

                key = Console.ReadKey(false);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selection = selection == 0 ? options.Count - 1 : selection - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        selection = selection == options.Count - 1 ? 0 : selection + 1;
                        break;

                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
            }
            return options.ElementAt(selection).Value;
        }

        public static void DisplayCarActions(Dictionary<string, Delegate> actions)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nUse ⬆️  and ⬇️  to navigate and press \u001b[32mEnter/Return\u001b[0m to advance turn:");
            (int left, int top) = Console.GetCursorPosition();

            List<string> actionList = new List<string>(actions.Keys);

            var selection = 0;
            var color = "✅ \u001b[32m";
            ConsoleKeyInfo key;
            bool isSelected = false;

            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);

                foreach (var act in actionList)
                {
                    Console.WriteLine($"{(selection == actionList.IndexOf(act) ? color : "   ")}{act}\u001b[0m");
                }

                key = Console.ReadKey(false);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selection = selection == 0 ? actionList.Count - 1 : selection - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        selection = selection == actionList.Count - 1 ? 0 : selection + 1;
                        break;

                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
            }

            string selectedAction = actionList[selection];
            Console.WriteLine($"\n{color}The car {selectedAction}ed");

            // Execute the selected action delegate
            actions[selectedAction].DynamicInvoke();

            Console.ReadLine();
        }

    }

}
