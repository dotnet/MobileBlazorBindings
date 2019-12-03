using System.Collections.Generic;

namespace Microsoft.MobileBlazorBindings.Elements.GridInternals
{
#pragma warning disable CA1812 // Internal class that is apparently never instantiated; this class is used for deserialization
    internal class GridMetadataForDeserialization
#pragma warning restore CA1812 // Internal class that is apparently never instantiated
    {
        public List<ColumnDefinitionMetadata> ColumnDefinitions { get; set; } = new List<ColumnDefinitionMetadata>();
        public List<RowDefinitionMetadata> RowDefinitions { get; set; } = new List<RowDefinitionMetadata>();
    }
}
