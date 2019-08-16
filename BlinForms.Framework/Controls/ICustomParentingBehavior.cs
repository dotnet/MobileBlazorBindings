using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    /// <summary>
    /// Indicates that a control has a custom behavior for determining where child controls should be parented to.
    /// For example, a SplitContainer control has built-in immutable children, and should not have those children
    /// created directly.
    /// </summary>
    internal interface ICustomParentingBehavior
    {
        void SetActualParentControl(Control parentControl);
        Control GetEffectiveParentControl();
    }
}
