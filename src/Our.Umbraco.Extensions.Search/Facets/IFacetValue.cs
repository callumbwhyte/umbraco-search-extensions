using System;

namespace Our.Umbraco.Extensions.Search.Facets
{
    public interface IFacetValue<T>
    {
        public T Value { get; }

        public int Count { get; }
    }
}