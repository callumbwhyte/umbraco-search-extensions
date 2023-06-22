using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Examine;
using Our.Umbraco.Extensions.Search.Composing;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Models.PublishedContent;

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
                .Select(ConvertValue<T>)
                .Where(x => x != null);
        }

        /// <summary>
        /// Get the value for a particular field in the results
        /// </summary>
        public static string Value(this ISearchResult result, string field)
        {
            result.Values.TryGetValue(field, out string value);

            return value;
        }

        /// <summary>
        /// Get the value for a particular field in the results
        /// </summary>
        public static T Value<T>(this ISearchResult result, string field)
        {
            result.Values.TryGetValue(field, out string value);

            return ConvertValue<T>(value);
        }

        /// <summary>
        /// Get multiple values for a particular field in the results
        /// </summary>
        public static IEnumerable<T> Values<T>(this ISearchResult result, string field)
        {
            var values = result.GetValues(field);

            return values
                .Select(ConvertValue<T>)
                .Where(x => x != null);
        }

        private static T ConvertValue<T>(object value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            if (converter.CanConvertFrom(value.GetType()) == true)
            {
                return (T)converter.ConvertFrom(value);
            }

            var mapper = ServiceLocator.GetInstance<IUmbracoMapper>();

            if (typeof(IPublishedContent).IsAssignableFrom(typeof(T)) == true)
            {
                return (T)mapper.Map<IPublishedContent>(value);
            }

            return mapper.Map<T>(value);
        }
    }
}