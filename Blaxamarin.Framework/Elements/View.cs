using Emblazon;
using System.Globalization;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class View : VisualElement
    {
        public XF.LayoutOptions? HorizontalOptions { get; set; }
        public XF.Thickness? Margin { get; set; }
        public XF.LayoutOptions? VerticalOptions { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (HorizontalOptions != null)
            {
                // TODO: Create helper for this
                builder.AddAttribute(nameof(HorizontalOptions),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "{0},{1}",
                        (int)HorizontalOptions.Value.Alignment,
                        HorizontalOptions.Value.Expands));
            }
            if (Margin != null)
            {
                // TODO: Create helper for this
                builder.AddAttribute(nameof(Margin),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "{0},{1},{2},{3}",
                        Margin.Value.Left,
                        Margin.Value.Top,
                        Margin.Value.Right,
                        Margin.Value.Bottom));
            }
            if (VerticalOptions != null)
            {
                // TODO: Create helper for this
                builder.AddAttribute(nameof(VerticalOptions),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "{0},{1}",
                        (int)VerticalOptions.Value.Alignment,
                        VerticalOptions.Value.Expands));
            }
        }
    }
}
