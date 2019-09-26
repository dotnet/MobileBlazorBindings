using Emblazon;
using System.Diagnostics;
using Xamarin.Forms;

namespace Blaxamarin.Framework
{
    internal class BlaxamarinElementManager : ElementManager<IXamarinFormsElementHandler>
    {
        protected override bool IsParented(IXamarinFormsElementHandler handler)
        {
            return handler.ElementControl.Parent != null;
        }

        protected override void AddChildElement(
            IXamarinFormsElementHandler parentHandler,
            IXamarinFormsElementHandler childHandler,
            int physicalSiblingIndex)
        {
            // TODO: What is the set of types that support child elements? Do they all need to be special-cased here? (Maybe...)

            var parent = parentHandler.ElementControl;
            var child = childHandler.ElementControl;

            switch (parent)
            {
                case Layout<View> parentAsLayout:
                    {
                        var childAsView = child as View;

                        if (physicalSiblingIndex <= parentAsLayout.Children.Count)
                        {
                            parentAsLayout.Children.Insert(physicalSiblingIndex, childAsView);
                        }
                        else
                        {
                            Debug.WriteLine($"WARNING: {nameof(AddChildElement)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but parentAsLayout.Children.Count={parentAsLayout.Children.Count}");
                            parentAsLayout.Children.Add(childAsView);
                        }
                    }
                    break;
                case ContentView parentAsContentView:
                    {
                        var childAsView = child as View;
                        parentAsContentView.Content = childAsView;
                    }
                    break;
                case ContentPage parentAsContentPage:
                    {
                        var childAsView = child as View;
                        parentAsContentPage.Content = childAsView;
                    }
                    break;
                case TabbedPage parentAsTabbedPage:
                    {
                        var childAsPage = child as Page;

                        if (physicalSiblingIndex <= parentAsTabbedPage.Children.Count)
                        {
                            parentAsTabbedPage.Children.Insert(physicalSiblingIndex, childAsPage);
                        }
                        else
                        {
                            Debug.WriteLine($"WARNING: {nameof(AddChildElement)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but parentAsTabbedPage.Children.Count={parentAsTabbedPage.Children.Count}");
                            parentAsTabbedPage.Children.Add(childAsPage);
                        }
                    }
                    break;
                case ScrollView parentAsScrollView:
                    {
                        var childAsView = child as View;
                        parentAsScrollView.Content = childAsView;
                    }
                    break;
                case Application parentAsApp:
                    {
                        if (parentAsApp.MainPage != null)
                        {
                            Debug.Fail($"Application already has MainPage set; cannot set {parentAsApp.GetType().FullName}'s MainPage to {child.GetType().FullName}");
                        }
                        else
                        {
                            if (child is Page childControlAsPage)
                            {
                                parentAsApp.MainPage = childControlAsPage;
                            }
                            else
                            {
                                Debug.Fail($"Application MainPage must be a Page; cannot set {parentAsApp.GetType().FullName}'s MainPage to {child.GetType().FullName}");
                            }
                        }
                    }
                    break;
                case MasterDetailPage masterDetailPage:
                    {
                        if (childHandler is Elements.Handlers.MasterPageHandler masterPage)
                        {
                            masterDetailPage.Master = masterPage.PageControl;
                        }
                        else if (childHandler is Elements.Handlers.DetailPageHandler detailPage)
                        {
                            masterDetailPage.Detail = detailPage.PageControl;
                        }
                        else
                        {
                            Debug.Fail($"Unknown child type {child.GetType().FullName} being added to parent element type {parent.GetType().FullName}.");
                        }
                    }
                    break;
                default:
                    Debug.Fail($"Don't know how to handle parent element type {parent.GetType().FullName} in order to add child {child.GetType().FullName}");
                    break;
            }
        }

        protected override int GetPhysicalSiblingIndex(
            IXamarinFormsElementHandler handler)
        {
            // TODO: What is the set of types that support child elements? Do they all need to be special-cased here? (Maybe...)
            var nativeComponent = handler.ElementControl;

            switch (nativeComponent.Parent)
            {
                case Layout<View> parentAsLayout:
                    {
                        var childAsView = nativeComponent as View;
                        return parentAsLayout.Children.IndexOf(childAsView);
                    }
                case ContentView _:
                    {
                        // A ContentView can have only 1 child, so the child's index is always 0.
                        return 0;
                    }
                case ContentPage _:
                    {
                        // A ContentPage can have only 1 child, so the child's index is always 0.
                        return 0;
                    }
                case TabbedPage tabbedPage:
                    {
                        var childAsPage = nativeComponent as Page;
                        return tabbedPage.Children.IndexOf(childAsPage);
                    }
                case ScrollView _:
                    {
                        // A ScrollView can have only 1 child, so the child's index is always 0.
                        return 0;
                    }
                case Application _:
                    {
                        // An Application can have only 1 child (its MainPage), so the child's index is always 0.
                        return 0;
                    }
                default:
                    Debug.Fail($"Don't know how to handle parent element type {nativeComponent.Parent.GetType().FullName} in order to get index of sibling {nativeComponent.GetType().FullName}");
                    return -1;
            }
        }

        protected override void RemoveElement(IXamarinFormsElementHandler handler)
        {
            // TODO: Need to make this logic more generic; not all parents are Layouts, not all children are Views
           
            var control = handler.ElementControl;
            var physicalParent = control.Parent;
            if (physicalParent is Layout<View> physicalParentAsLayout)
            {
                var childTargetAsView = control as View;
                physicalParentAsLayout.Children.Remove(childTargetAsView);
            }
        }

        protected override bool IsParentOfChild(IXamarinFormsElementHandler parentHandler, IXamarinFormsElementHandler childHandler)
        {
            return childHandler.ElementControl.Parent == parentHandler.ElementControl;
        }
    }
}
