using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.UI.Menu
{
    class InfoEditorMenu : Menu
    {
        private int currFocus;
        private bool isActiveFocus;
        private Logic.Task currTask;

        private Components.InputLabel[] inputLabels;

        public InfoEditorMenu(int idxOfCurrTask)
            : base()
        {
            // Добавляем обработчиков
            OnInput += InputSwitchFocusedInputLabel;
            OnInput += EnterFocusInputLabel;
            OnInput += InputCloseEditor;

            // Инициализируем значения
            currTask = Logic.Logic.ListOfTasks[idxOfCurrTask];
            currFocus = -1;
            isActiveFocus = false;
            inputLabels = new Components.InputLabel[2];

            inputLabels[0] = new Components.InputLabel(currTask.Name, 50, 2, 49);
            inputLabels[1] = new Components.InputLabel(currTask.Description, 52, 5, 47);

            // Добавляем компоненты
            UIComponents.Add("TaskInfoPanel",
                new Components.Panel(40, 0, 60, 30)
            );
            UIComponents.Add("LabelForTaskInfoPanel",
                new Components.Label("Подробная информация", 60, 0)
            );
            UIComponents.Add("NameLabel",
                new Components.Label("Задача: ", 42, 2)
            );
            UIComponents.Add("DescrLabel",
                new Components.Label("Описание: ", 42, 5)
            );
            UIComponents.Add("CheckLabel",
                new Components.Label(MakeCheckLabelString(), 42, 25)
            );

        }

        private void EnterFocusInputLabel(ConsoleKey key)
        {
            if (key != ConsoleKey.Enter)
                return;
            if (currFocus >= 0)
                isActiveFocus = true;
        }

        private void InputCloseEditor(ConsoleKey key)
        {
            if (key != ConsoleKey.Escape && key != ConsoleKey.H)
                return;
            UpdateTask();
            UI.CloseCurrMenu();
        }

        private void InputSwitchFocusedInputLabel(ConsoleKey key)
        {
            int prevFocus = currFocus;
            if (key == ConsoleKey.J)
            {
                currFocus++;
                if (currFocus == inputLabels.Length)
                    currFocus = -1;
            }
            else if (key == ConsoleKey.K)
            {
                currFocus--;
                if (currFocus == -2)
                    currFocus = inputLabels.Length - 1;
            }
            else
                return;

            if (prevFocus >= 0)
                inputLabels[prevFocus].focused = false;
            if (currFocus >= 0)
                inputLabels[currFocus].focused = true;
            UpdateTask();
        }
        private void UpdateTask()
        {
            currTask.Name = inputLabels[0].Text.ToString();
            currTask.Description = inputLabels[1].Text.ToString();
        }

        private string MakeCheckLabelString()
        {
            return "Статус: " + (currTask.IsChecked ? "[x]" : "[ ]");
        }

        public override void Draw(Frame frame)
        {
            DrawComponetns(frame);
            DrawInfoOfCurrentTask(frame);
        }

        private void DrawInfoOfCurrentTask(Frame frame)
        {
            foreach (var label in inputLabels)
            {
                label.Draw(frame);
            }
        }

        public override void Update(ConsoleKeyInfo keyInfo)
        {
            ConsoleKey key = keyInfo.Key;
            if (isActiveFocus)
            {
                if (key == ConsoleKey.Escape)
                {
                    isActiveFocus = false;
                    return;
                }
                inputLabels[currFocus].Update(keyInfo);
            }
            else
                OnInput?.Invoke(key);
        }
    }
}
