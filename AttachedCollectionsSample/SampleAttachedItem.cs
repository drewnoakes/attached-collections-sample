using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Imaging.Interop;

namespace AttachedCollectionsSample
{
    internal sealed class SampleAttachedItem
        : ITreeDisplayItem,
          ITreeDisplayItemWithImages,
          // NOTE all the interfaces beneath here are optional
          IPrioritizedComparable,
          IInteractionPatternProvider,
          IBrowsablePattern,
          IContextMenuPattern,
          INotifyPropertyChanged
    {
        #region ITreeDisplayItem

        public string Text => "Sample item";

        public string ToolTipText => "Sample tool tip text";

        public string StateToolTipText => "State tool tip text";

        public object ToolTipContent => null;

        public FontWeight FontWeight => FontWeights.Normal;

        public FontStyle FontStyle => FontStyles.Normal;

        public bool IsCut => false;

        #endregion

        #region ITreeDisplayItemWithImages

        // Select images at random

        public ImageMoniker IconMoniker => SelectRandom(s_monikers);

        public ImageMoniker ExpandedIconMoniker => SelectRandom(s_monikers);

        public ImageMoniker OverlayIconMoniker => SelectRandom(s_overlays);

        public ImageMoniker StateIconMoniker => SelectRandom(s_states);

        // A subset of known monikers, for demo purposes
        private static readonly ImageMoniker[] s_monikers =
        {
            KnownMonikers.AboutBox,
            KnownMonikers.PageLayout,
            KnownMonikers.ShowStartWindow,
            KnownMonikers.TableOK
        };

        // For the overlay, only a subset of the known monikers make sense, and they are shown here.
        // Other images can be used, but should be tested on a case-by-case basis. The expectation
        // is that the overlay appears in the lower-right corner of the icon.
        private static readonly ImageMoniker[] s_overlays =
        {
            KnownMonikers.OverlayAlert,
            KnownMonikers.OverlayError,
            KnownMonikers.OverlayExcluded,
            KnownMonikers.OverlayFriend,
            KnownMonikers.OverlayLock,
            KnownMonikers.OverlayLoginDisabled,
            KnownMonikers.OverlayWarning
        };
        
        // For the state icon, only a subset of the known monikers work. These are smaller (7x16) than most other monikers (16x16).
        // Attempting to use a 16x16 icon here will result in an icon that appears squashed and likely illegible.
        private static readonly ImageMoniker[] s_states =
        {
            KnownMonikers.CheckedInNode,
            KnownMonikers.CheckedOutForEditNode,
            KnownMonikers.CheckedOutByOtherUserNode,
            KnownMonikers.PendingChangeNode,
            KnownMonikers.PendingDeleteNode,
            KnownMonikers.PendingMergeNode,
            KnownMonikers.PendingRenameNode,
            KnownMonikers.PendingUndeleteNode,
            KnownMonikers.SourceControlEditableNode,
            KnownMonikers.SourceControlExcludedNode,
            KnownMonikers.SourceControlOrphanedNode
        };
        
        private static readonly Random s_random = new Random();
        
        private static ImageMoniker SelectRandom(ImageMoniker[] monikers) => monikers[s_random.Next(0, monikers.Length)];

        #endregion

        #region IPrioritizedComparable

        public int Priority => 0;

        public int CompareTo(object obj)
        {
            // TODO implement this
            // Here you can compare against other items in your attached collection
            // to control the sort order.
            return 0;
        }

        #endregion

        #region IInteractionPatternProvider

        // Other patterns you may wish to implement:
        //
        // - IInvocationPattern
        // - ISupportExpansionEvents
        // - ISupportExpansionState
        // - IDragDropSourcePattern
        // - IDragDropTargetPattern
        // - ISupportDisposalNotification
        // - IRenamePattern
        // - IPivotItemProviderPattern
        //
        // NOTE we don't have to support ITreeDisplayItemWithImages -- it's covered by ITreeDisplayItem

        private static readonly HashSet<Type> s_supportedPatterns = new HashSet<Type>()
        {
            typeof(ITreeDisplayItem),
            typeof(IBrowsablePattern),
            typeof(IContextMenuPattern)
        };

        TPattern IInteractionPatternProvider.GetPattern<TPattern>()
        {
            if (s_supportedPatterns.Contains(typeof(TPattern)))
            {
                return this as TPattern;
            }

            return null;
        }

        #endregion

        #region IBrowsablePattern

        public object GetBrowseObject()
        {
            // Return an object here that will be displayed in the "Properties" pane
            return null;
        }

        #endregion

        #region IContextMenuPattern

        public IContextMenuController ContextMenuController => null;

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        #endregion
    }
}
