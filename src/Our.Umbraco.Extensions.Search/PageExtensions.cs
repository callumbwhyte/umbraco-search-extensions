using System;
using System.Collections.Generic;
using System.Linq;
using Examine;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Our.Umbraco.Extensions.Search
{
    public static class PageExtensions
    {
        /// <summary>
        /// Get paged results from the given results
        /// </summary>
        public static IEnumerable<ISearchResult> Page(this ISearchResults results, int page, int perPage, out int totalResults)
        {
            return results.Page(page, perPage, out _, out totalResults);
        }

        /// <summary>
        /// Get paged results from the given results
        /// </summary>
        public static IEnumerable<ISearchResult> Page(this ISearchResults results, int page, int perPage, out int totalPages, out int totalResults)
        {
            totalPages = (int)Math.Ceiling((decimal)results.TotalItemCount / perPage);

            totalResults = (int)results.TotalItemCount;

            return results.Skip((page - 1) * perPage).Take(perPage);
        }

        /// <summary>
        /// Get paged results from the given results
        /// </summary>
        public static IEnumerable<T> Page<T>(this ISearchResults results, int page, int perPage, out int totalResults)
            where T : IPublishedContent
        {
            return results.Page<T>(page, perPage, out _, out totalResults);
        }

        /// <summary>
        /// Get paged results from the given results
        /// </summary>
        public static IEnumerable<T> Page<T>(this ISearchResults results, int page, int perPage, out int totalPages, out int totalResults)
            where T : IPublishedContent
        {
            totalPages = (int)Math.Ceiling((decimal)results.TotalItemCount / perPage);

            totalResults = (int)results.TotalItemCount;

            return results
                .Skip((page - 1) * perPage)
                .GetResults<T>()
                .Take(perPage);
        }
    }
}