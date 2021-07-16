using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.UI
{
    static class UI
    {
        static private bool IsRun;
        static private int CurrIdx;
        static UI()
        {
            IsRun = true;
            CurrIdx = 0;
        }

        static public void Run()
        {
            while(IsRun)
            {
                string cmd = Console.ReadLine();
                switch (cmd)
                {
                    case "list":
                        CmdList();
                        break;
                    case "create":
                        CmdCreate();
                        break;
                    case "remove":
                        CmdRemove();
                        break;
                    case "check":
                        CmdCheck();
                        break;
                    default:
                        break;
                }
            }
        }

        static void CmdList()
        {
            foreach (var task in Logic.Logic.ListOfTasks)
            {
                Console.WriteLine($"Name: {task.Name}\n{task.Description}\nIs done: {task.IsChecked.ToString()}\n\n");
            }
        }

        static void CmdCreate()
        {
            string name = Console.ReadLine();
            string description = Console.ReadLine();
            var task = new Logic.Task(name, description);

            Logic.Logic.AddTask(task);
        }

        static void CmdRemove()
        {
            Logic.Logic.RemoveTask(CurrIdx);
        }

        static void CmdCheck()
        {
            Logic.Logic.CheckTask(CurrIdx);
        }
    }
}
