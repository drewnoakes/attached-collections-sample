using System.Collections;

using Microsoft.VisualStudio.Shell;

namespace AttachedCollectionsSample
{
    internal sealed class SampleAttachedCollectionSource : IAttachedCollectionSource
    {
        public SampleAttachedCollectionSource(IVsHierarchyItem hierarchyItem)
        {
            SourceItem = hierarchyItem;
        }

        public object SourceItem { get; }

        // If the loading of items is asynchronous, you can implement IAsyncAttachedCollectionSource instead
        // which itself extends INotifyPropertyChanged, then you can fire a property change notification for
        // HasItems so show/hide the small triangle that indicates whether the node may be expanded to show
        // children.

        public bool HasItems => true;

        // If the item collection implements INotifyCollectionChanged (e.g. ObservableCollection<T>)
        // then it may add and remove child items over time.

        public IEnumerable Items => new object[] { new SampleAttachedItem() };
    }
}
