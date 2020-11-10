// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace HybridAuthApp
{
    public class CounterState
    {
        public int CurrentCount { get; private set; }

        public void IncrementCount()
        {
            CurrentCount++;
            StateChanged?.Invoke();
        }

        public event Action StateChanged;
    }
}
