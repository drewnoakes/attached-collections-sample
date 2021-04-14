
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;

namespace AttachedCollectionsSample
{
    internal static class Relationships
    {
        public static IAttachedRelationship Contains { get; } = new ContainsAttachedRelationship();

        public static IAttachedRelationship ContainedBy { get; } = new ContainedByAttachedRelationship();

        private sealed class ContainsAttachedRelationship : IAttachedRelationship
        {
            public string Name => KnownRelationships.Contains;
            public string DisplayName => KnownRelationships.Contains;
        }

        private sealed class ContainedByAttachedRelationship : IAttachedRelationship
        {
            public string Name => KnownRelationships.ContainedBy;
            public string DisplayName => KnownRelationships.ContainedBy;
        }
    }
}
