using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.UI
{
    static class UI
    {
        static private bool IsRun;

        static private Menu.Menu CurrMenu;
        static private Stack<Menu.Menu> StackOfMenu;

        static private Frame CurrFrame;
        static private Frame PrevFrame;

        static private readonly int maxW = 101;
        static private readonly int maxH = 51;

        static UI()
        {
            IsRun = true;

            StackOfMenu = new Stack<Menu.Menu>();
            CurrMenu = new Menu.MainMenu();

            Console.SetWindowSize(maxW+1, maxH+1);
            Console.SetBufferSize(maxW + 1, maxH + 1);
            CurrFrame = new Frame(maxH, maxW);
            PrevFrame = new Frame(maxH, maxW);

        }

        static public void Run()
        {
            CurrMenu?.Draw(CurrFrame);
            UpdateConsole();

            while (IsRun)
            {
                // Обработка пользовательского ввода
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                CurrMenu?.Update(keyInfo);

                // Отрисовка
                foreach (var menu in StackOfMenu)
                    menu.Draw(CurrFrame);
                CurrMenu?.Draw(CurrFrame);

                UpdateConsole();
            }
        }
        static private void UpdateConsole()
        {
            for (int y = 0; y < CurrFrame.H; ++y)
            {
                for (int x = 0; x < CurrFrame.W; ++x)
                {
                    if (CurrFrame[y, x] != PrevFrame[y, x])
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = CurrFrame[y, x].ForeGroundColor;
                        Console.BackgroundColor = CurrFrame[y, x].BackGroundColor;
                        Console.Write(CurrFrame[y, x].Char);
                    }
                    PrevFrame[y, x].Char = CurrFrame[y, x].Char;
                    PrevFrame[y, x].ForeGroundColor = CurrFrame[y, x].ForeGroundColor;
                    PrevFrame[y, x].BackGroundColor = CurrFrame[y, x].BackGroundColor;

                    CurrFrame[y, x].Char = ' ';
                    CurrFrame[y, x].ForeGroundColor = ConsoleColor.White;
                    CurrFrame[y, x].BackGroundColor = ConsoleColor.Black;
                }
            }
            Console.SetCursorPosition(0, maxH);
        }

        static public void OpenMenu(Menu.Menu menu)
        {
            StackOfMenu.Push(CurrMenu);
            CurrMenu = menu;
        }

        static public void CloseCurrMenu()
        {
            if (StackOfMenu.Count > 0)
                CurrMenu = StackOfMenu.Pop();
            else
            {
                IsRun = false;
                CurrMenu = null;
            }
        }
    }
}
