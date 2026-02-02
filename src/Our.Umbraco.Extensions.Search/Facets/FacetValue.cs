using System;

namespace Our.Umbraco.Extensions.Search.Facets
{
    internal class FacetValue<T> : IFacetValue<T>
    {
        public T Value { get; init; } = default!;

        public int Count { get; init; }
    }
}