// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using SQLite;

namespace BlazorBindingsToDo
{
    public class TodoItem
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }

        public bool IsDone { get; set; }

        public string Text { get; set; }

        public string Notes { get; set; }
    }
}
