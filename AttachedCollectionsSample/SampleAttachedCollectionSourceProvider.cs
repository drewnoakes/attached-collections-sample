using System.Collections.Generic;
using System.ComponentModel.Composition;

using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Utilities;

namespace AttachedCollectionsSample
{
    [AppliesToProject(".NET")]
    [Export(typeof(IAttachedCollectionSourceProvider))]
    [Name(nameof(SampleAttachedCollectionSourceProvider))]
    [Order(Before = HierarchyItemsProviderNames.Contains)]
    internal sealed class SampleAttachedCollectionSourceProvider : IAttachedCollectionSourceProvider
    {
        [ImportingConstructor]
        public SampleAttachedCollectionSourceProvider()
        {
        }

        public IAttachedCollectionSource CreateCollectionSource(object item, string relationshipName)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (relationshipName == KnownRelationships.Contains)
            {
                if (item is IVsHierarchyItem hierarchyItem)
                {
                    // Evaluate the hierarchy item to determine if you want to attach children to it.

                    // You can inspect the hierarchy, for example.
                    IVsHierarchyItemIdentity identity = hierarchyItem.HierarchyIdentity;

                    if (identity.NestedHierarchy.GetProperty(identity.NestedItemID, (int)__VSHPROPID.VSHPROPID_Name, out object name) == 0)
                    {
                    }

                    if (identity.NestedHierarchy.GetProperty(identity.NestedItemID, (int)__VSHPROPID7.VSHPROPID_ProjectTreeCapabilities, out object value) == 0)
                    {
                        // An item's ProjectTree capabilities is a space-separated list of well-known strings.
                        // These are often a better way of identifying the node than its caption.
                    }
                     
                    return new SampleAttachedCollectionSource(hierarchyItem);
                }

                // This provider will also be given the opportunity to attach children to
                // items it previously returned. In this way, it may build up multiple
                // levels of items in Solution Explorer.
            }

            // KnownRelationships.ContainedBy will be observed during Solution Explorer search,
            // where each attached item reports its parent(s) so that they may be displayed in the tree.
            // Search occurs via a MEF export of Microsoft.Internal.VisualStudio.PlatformUI.ISearchProvider.

            return null;
        }

        public IEnumerable<IAttachedRelationship> GetRelationships(object item)
        {
            // We want to attach children
            
            yield return Relationships.Contains;

            // If this item is set up to participate in Solution Explorer search, return
            // "ContainedBy" as well.

            // yield return Relationships.ContainedBy;
        }
    }
}
