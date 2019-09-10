using Microsoft.AspNetCore.Components;
using System;

namespace BlinForms.Framework
{
    public class BlinFormsMainFormType<TComponent> : IBlinFormsMainFormType
        where TComponent : IComponent
    {
        public BlinFormsMainFormType()
        {
            MainFormType = typeof(TComponent);
        }

        public Type MainFormType { get; }
    }
}
