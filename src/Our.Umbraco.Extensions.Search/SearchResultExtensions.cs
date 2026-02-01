using System.Collections.Generic;
using System.Linq;
using Examine;
using Our.Umbraco.Extensions.Search.Helpers;

namespace Our.Umbraco.Extensions.Search
{
    public static class SearchResultExtensions
    {
        /// <summary>
        /// Get the results converted to the given type
        /// </summary>
        public static IEnumerable<T> GetResults<T>(this IEnumerable<ISearchResult> results)
        {
            return results
                .Select(ValueMapperHelper.Instance.ConvertValue<T>)
                .Where(x => x != null)!;
        }

        /// <summary>
        /// Get the value for a particular field in the results
        /// </summary>
        public static string? Value(this ISearchResult result, string field)
        {
            result.Values.TryGetValue(field, out var value);

            return value;
        }

        /// <summary>
        /// Get the value for a particular field in the results
        /// </summary>
        public static T? Value<T>(this ISearchResult result, string field)
        {
            result.Values.TryGetValue(field, out var value);

            return ValueMapperHelper.Instance.ConvertValue<T>(value);
        }

        /// <summary>
        /// Get multiple values for a particular field in the results
        /// </summary>
        public static IEnumerable<T> Values<T>(this ISearchResult result, string field)
        {
            var values = result.GetValues(field);

            return values
                .Select(ValueMapperHelper.Instance.ConvertValue<T>)
                .Where(x => x != null)!;
        }
    }
}