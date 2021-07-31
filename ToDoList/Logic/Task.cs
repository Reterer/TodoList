using System;

namespace ToDoList.Logic
{
    [Serializable]
    class Task
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public bool IsChecked { get; set; }

        public Task(string Name, string Description, bool IsChecked)
        {
            this.Name = Name;
            this.Description = Description;
            this.IsChecked = IsChecked;

        }

        public Task(string Name, string Description)
            : this(Name, Description, false)
        { }

        public override string ToString()
        {
            return "Task: " + Name + "\n" +
                   "\tDesc: " + Description + "\n" +
                   "\tIs done: [" + (IsChecked ? 'x' : ' ') + "]\n\n";
        }
    }
}
