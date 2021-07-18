using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.UI.Menu
{
    class RemoveTaskMenu : Menu
    {
        private int IdxOfRemovedTask;
        private bool IsActiveYes;

        public RemoveTaskMenu(int idxOfRemoveTask)
            : base()
        {
            this.IdxOfRemovedTask = idxOfRemoveTask;
            this.IsActiveYes = false;
            // Добавляем обраотчики
            OnInput = InputYesOrNo;

            // Добавляем компоненты
            UIComponents.Add("MainPanel",
                new Components.Panel(35, 20, 30, 5)              
            );

            string textOfMainLabel = $"Удалить {Logic.Logic.ListOfTasks[idxOfRemoveTask].Name} ?";
            UIComponents.Add("MainLabel",
                new Components.Label(textOfMainLabel, 15 - textOfMainLabel.Length / 2 + 35, 21)
            );
        }

        private void InputYesOrNo(ConsoleKey key)
        {
            if(key == ConsoleKey.J || key == ConsoleKey.K || 
               key == ConsoleKey.H || key == ConsoleKey.L)
            {
                IsActiveYes = !IsActiveYes;
                return;
            }

            else if(key == ConsoleKey.Enter)
            {
                if(IsActiveYes)
                    Logic.Logic.RemoveTask(IdxOfRemovedTask);
            }
            else if(key == ConsoleKey.Escape)
            {
                // Nothing
            }
            else
                return;

            UI.CloseCurrMenu();
        }

        public override void Draw(Frame frame)
        {
            DrawComponetns(frame);
            NewMethod(frame);
        }

        private void NewMethod(Frame frame)
        {
            var YesBtn = new Components.Label(" ДА ", 44, 23, 4);
            var NoBtn = new Components.Label(" НЕТ ", 52, 23, 5);

            if (IsActiveYes)
            {
                YesBtn.BackgroundColor = Theme.HighlightColorBg;
                NoBtn.BackgroundColor = Theme.DefaultColorBg;
            }
            else
            {
                YesBtn.BackgroundColor = Theme.DefaultColorBg;
                NoBtn.BackgroundColor = Theme.HighlightColorBg;
            }

            YesBtn.Draw(frame);
            NoBtn.Draw(frame);
        }

        public override void Update(ConsoleKeyInfo keyInfo)
        {
            OnInput?.Invoke(keyInfo.Key);
        }
    }
}
