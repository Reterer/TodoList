using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.UI.Menu
{
    abstract class Menu
    {
        protected Action<ConsoleKey> OnInput;
        protected Dictionary<String, Components.Component> UIComponents;

        protected Menu()
        {
            UIComponents = new Dictionary<string, Components.Component>();
            OnInput = null;
        }

        abstract public void Update(ConsoleKeyInfo keyInfo);
        abstract public void Draw(Frame frame);
        protected void DrawComponetns(Frame frame)
        {
            foreach (var item in UIComponents)
            {
                item.Value.Draw(frame);
            }
        }
    }
}
