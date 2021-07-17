using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.UI.Menu
{
    class MainMenu : Menu    
    {
        private int idxOfCurrTask;

        public MainMenu()
            : base()
        {
            // Обработчики Нажатий клавиш
            OnInput = InputAdd;
            OnInput += InputRemove;
            OnInput += InputCheck;
            OnInput += InputEdite;
            OnInput += InputExit;
            OnInput += InputDown;
            OnInput += InputUp;

            // Копоненты меню, отображаемые в терминале
            UIComponents.Add("TaskBoardPanel", 
                new Components.Panel(0, 0, 39, 30)
            );
            UIComponents.Add("TaskInfoPanel", 
                new Components.Panel(40, 0, 60, 30)
            );
            UIComponents.Add("LabelForTaskBoardPanel",
                new Components.Label("Список задач", 14, 0)
            );
            UIComponents.Add("LabelForTaskInfoPanel",
                new Components.Label("Подробная информация", 60, 0)
            );

            idxOfCurrTask = 0;
        }

        private void Fixed()
        {
            if (idxOfCurrTask < 0 || idxOfCurrTask >= Logic.Logic.ListOfTasks.Count)
                idxOfCurrTask = 0;
        }
        public override void Update(ConsoleKey key)
        {
            Fixed();
            OnInput?.Invoke(key);
        }
        public override void Draw(Frame frame)
        {
            Fixed();
            DrawComponetns(frame);
            DrawListOfTasks(frame);
            DrawInfoOfCurrentTask(frame);
        }

        private void DrawInfoOfCurrentTask(Frame frame)
        {
            if (Logic.Logic.ListOfTasks.Count == 0)
                return;
            // Распечатка информации о выбранной задаче
            var currTask = Logic.Logic.ListOfTasks[idxOfCurrTask];

            var NameLabel = new Components.Label(currTask.Name, 42, 2);
            var DescriptionLabel = new Components.Label(currTask.Description, 42, 5);
            var IsChecked = new Components.Label(currTask.IsChecked ? "[x]" : "[ ]", 42, 25);

            NameLabel.Draw(frame);
            DescriptionLabel.Draw(frame);
            IsChecked.Draw(frame);

        }

        private void DrawListOfTasks(Frame frame)
        {
            // Распечатка списка задач
            var ListOftasks = Logic.Logic.ListOfTasks;
            var Label = new Components.Label("", 1, 2, 38);

            for (int i = 0; i < ListOftasks.Count; ++i)
            {
                Label.Text = (i == idxOfCurrTask ? "> " : "  ") +
                             (ListOftasks[i].IsChecked ? "[x]" : "[ ]") +
                             " " + ListOftasks[i].Name;

                if (i == idxOfCurrTask)
                    Label.BackgroundColor = Theme.HighlightColorBg;
                else
                    Label.BackgroundColor = Theme.DefaultColorBg;

                Label.Draw(frame);
                Label.Y += 2;
            }
        }

        private void InputAdd(ConsoleKey key)
        {
            if (key != ConsoleKey.A)
                return;

            var newTask = new Logic.Task("Новая задача", "");
            Logic.Logic.AddTask(idxOfCurrTask, newTask);
        }

        private void InputRemove(ConsoleKey key)
        {
            if (key != ConsoleKey.R)
                return;
            if (Logic.Logic.ListOfTasks.Count == 0)
                return;

            UI.OpenMenu(new RemoveTaskMenu(idxOfCurrTask));
        }

        private void InputCheck(ConsoleKey key)
        {
            if (key != ConsoleKey.C)
                return;
            if (Logic.Logic.ListOfTasks.Count == 0)
                return;

            Logic.Logic.CheckTask(idxOfCurrTask);
        }

        private void InputEdite(ConsoleKey key)
        {
            if (key != ConsoleKey.Enter || key != ConsoleKey.E)
                return;
            if (Logic.Logic.ListOfTasks.Count == 0)
                return;

        }

        private void InputExit(ConsoleKey key)
        {
            if (key != ConsoleKey.Escape)
                return;
            UI.CloseCurrMenu();
        }

        private void InputDown(ConsoleKey key)
        {
            if (key != ConsoleKey.J)
                return;
            if (Logic.Logic.ListOfTasks.Count == 0)
                return;

            idxOfCurrTask--;
            if (idxOfCurrTask == -1)
                idxOfCurrTask = Logic.Logic.ListOfTasks.Count - 1;
        }

        private void InputUp(ConsoleKey key)
        {
            if (key != ConsoleKey.K)
                return;
            if (Logic.Logic.ListOfTasks.Count == 0)
                return;

            idxOfCurrTask = (idxOfCurrTask + 1) % Logic.Logic.ListOfTasks.Count;
        }
    }
}
