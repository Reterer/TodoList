using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ToDoList.Logic
{
    static class Logic
    {
        static private List<Task> _ListOfTasks;

        static Logic()
        {
            _ListOfTasks = new List<Task>();
        }

        static public ReadOnlyCollection<Task> ListOfTasks 
        {
            get => new ReadOnlyCollection<Task>(_ListOfTasks);
        }
        static public void AddTask(int idx, Task task)
        {
            _ListOfTasks.Insert(idx, task);
        }

        static public void RemoveTask(int idxOfTask)
        {
            _ListOfTasks.RemoveAt(idxOfTask);
        }

        static public void CheckTask(int idxOfTask)
        {
            _ListOfTasks[idxOfTask].IsChecked = !_ListOfTasks[idxOfTask].IsChecked;
        }

        static public bool Save(string path)
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, _ListOfTasks);
                return true;
            }

            return false;
        }

        static public bool Load(string path)
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream(path, FileMode.Open))
            {
                var obj = formatter.Deserialize(fs);
                List<Task> list = obj as List<Task>;

                if(list != null)
                {
                    _ListOfTasks = list;
                    return true;
                }

            }

            return false;
        }
    }
}
