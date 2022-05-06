// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    /// <summary>
    /// Helper class for managing cases where an intermediate component is used to connect a parent
    /// and child as a container for metadata. For example, the <see cref="Grid"/> component uses
    /// intermediate <see cref="GridCell"/> components to contain metadata about row and column locations.
    /// The <see cref="GridCell"/> component uses this type to connect the contents of the cells and the
    /// parent <see cref="Grid"/>.
    /// </summary>
    /// <typeparam name="TParent"></typeparam>
    /// <typeparam name="TChild"></typeparam>
    public class ParentChildManager<TParent, TChild>
        where TParent : class
        where TChild : class
    {
        private TParent _parent;
        private TChild _child;

        public ParentChildManager(Action<ParentChildManager<TParent, TChild>> onParentAndChildSet)
        {
            _onParentAndChildSet = onParentAndChildSet ?? throw new ArgumentNullException(nameof(onParentAndChildSet));
        }

        private readonly Action<ParentChildManager<TParent, TChild>> _onParentAndChildSet;

        public TChild Child
        {
            get => _child;
            set
            {
                _child = value;
                ChildChanged?.Invoke(this, EventArgs.Empty);
                TryDoOnParentAndChildSet();
            }
        }

        public TParent Parent
        {
            get => _parent;
            set
            {
                _parent = value;
                ParentChanged?.Invoke(this, EventArgs.Empty);
                TryDoOnParentAndChildSet();
            }
        }

        public event EventHandler ChildChanged;
        public event EventHandler ParentChanged;

        public void SetChild(object child)
        {
            if (child is null)
            {
                Child = null;
                return;
            }

            if (child is TChild childOfChildType)
            {
                Child = childOfChildType;
            }
            else
            {
                throw new ArgumentException($"Expected child to be of type {typeof(TChild).FullName} but it is of type {child.GetType().FullName}.", nameof(child));
            }
        }

        public void SetParent(object parent)
        {
            if (parent is null)
            {
                Parent = null;
                return;
            }

            if (parent is TParent parentOfParentType)
            {
                Parent = parentOfParentType;
            }
            else
            {
                throw new ArgumentException($"Expected parent to be of type {typeof(TParent).FullName} but it is of type {parent.GetType().FullName}.", nameof(parent));
            }
        }

        private void TryDoOnParentAndChildSet()
        {
            if (Parent != null && Child != null)
            {
                _onParentAndChildSet(this);
            }
        }
    }
}
