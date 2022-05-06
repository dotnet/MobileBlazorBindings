// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.IO;
using System.Threading.Tasks;

namespace BlazorBindingsToDo
{
    public class AppState
    {
        private TodoItemDatabase _todoDatabase;

        public TodoItemDatabase TodoDatabase
        {
            get
            {
                if (_todoDatabase == null)
                {
                    _todoDatabase =
                        new TodoItemDatabase(
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3"),
                            dbChanged: NotifyStateChanged);
                }
                return _todoDatabase;
            }
        }

        public int Counter { get; set; }

        public event Func<Task> OnChange;

        private async Task NotifyStateChanged() => await OnChange?.Invoke();
    }
}
