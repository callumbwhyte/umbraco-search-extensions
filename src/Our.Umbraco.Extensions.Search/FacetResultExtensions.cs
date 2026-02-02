using System;
using System.Collections.Generic;
using System.Linq;
using Examine;
using Examine.Search;
using Our.Umbraco.Extensions.Search.Facets;
using Our.Umbraco.Extensions.Search.Helpers;
using Umbraco.Extensions;

namespace Our.Umbraco.Extensions.Search
{
    public static class FacetResultExtensions
    {
        /// <summary>
        /// Get the values for a particular facet in the results
        /// </summary>
        public static IFacetResult? GetFacet(this IFacetResults results, string field)
        {
            results.Facets.TryGetValue(field, out var facet);

            return facet;
        }

        /// <summary>
        /// Get the values for a particular facet in the results
        /// </summary>
        public static IEnumerable<T> GetFacet<T>(this IFacetResults results, string field, Func<T, object>? orderSelector = null)
        {
            var facet = results.GetFacet(field);

            var values = facet?.GetValues<T>() ?? [];

            if (orderSelector != null)
            {
                return values.OrderBy(orderSelector);
            }

            return values;
        }

        /// <summary>
        /// Get the values for a particular facet in the results
        /// </summary>
        public static IEnumerable<T> GetFacet<T>(this ISearchResults results, string field, Func<T, object>? orderSelector = null)
        {
            if (results is not IFacetResults facetResults)
            {
                throw new NotSupportedException("Result does not support facets");
            }

            return facetResults.GetFacet(field, orderSelector);
        }

        /// <summary>
        /// Get the values and counts for a particular facet in the results
        /// </summary>
        public static IEnumerable<IFacetValue<T>> GetFacetCounts<T>(this IFacetResults results, string field, Func<T, object>? orderSelector = null)
        {
            var facet = results.GetFacet(field);

            var values = facet?.GetCounts<T>() ?? [];

            return values.OrderBy(x => orderSelector?.Invoke(x.Value) ?? x.Count);
        }

        /// <summary>
        /// Get the values and counts for a particular facet in the results
        /// </summary>
        public static IEnumerable<IFacetValue<T>> GetFacetCounts<T>(this ISearchResults results, string field, Func<T, object>? orderSelector = null)
        {
            if (results is not IFacetResults facetResults)
            {
                throw new NotSupportedException("Result does not support facets");
            }

            return facetResults.GetFacetCounts(field, orderSelector);
        }

        /// <summary>
        /// Get the values for a particular facet result
        /// </summary>
        public static IEnumerable<T> GetValues<T>(this IFacetResult result)
        {
            foreach (var facet in result)
            {
                var value = ValueMapperHelper.Instance.ConvertValue<T>(facet.Label);

                if (value == null)
                {
                    continue;
                }

                yield return value;
            }
        }

        /// <summary>
        /// Get the values and counts for a particular facet result
        /// </summary>
        public static IEnumerable<IFacetValue<T>> GetCounts<T>(this IFacetResult result)
        {
            foreach (var facet in result)
            {
                var value = ValueMapperHelper.Instance.ConvertValue<T>(facet.Label);

                if (value == null)
                {
                    continue;
                }

                yield return new FacetValue<T>
                {
                    Value = value,
                    Count = (int)facet.Value
                };
            }
        }
    }
}