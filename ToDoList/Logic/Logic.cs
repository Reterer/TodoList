using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ToDoList.Logic
{
    static class Logic
    {
        static private List<Task> ListOfTasks_;

        static Logic()
        {
            ListOfTasks_ = new List<Task>();
        }

        static public ReadOnlyCollection<Task> ListOfTasks 
        {
            get => new ReadOnlyCollection<Task>(ListOfTasks_);
        }
        static public void AddTask(Task task)
        {
            ListOfTasks_.Add(task);
        }

        static public void RemoveTask(int idxOfTask)
        {
            ListOfTasks_.RemoveAt(idxOfTask);
        }

        static public void CheckTask(int idxOfTask)
        {
            ListOfTasks_[idxOfTask].IsChecked = !ListOfTasks_[idxOfTask].IsChecked;
        }
    }
}
