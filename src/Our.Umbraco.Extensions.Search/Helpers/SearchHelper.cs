using System.Collections.Generic;
using System.Linq;
using Examine;
using Examine.Search;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Our.Umbraco.Extensions.Search.Helpers
{
    public class SearchHelper
    {
        /// <summary>
        /// Get the results for the given query
        /// </summary>
        public ISearchResults Search(IBooleanOperation query, out int totalResults)
        {
            var searchResult = query.Execute();

            totalResults = (int)searchResult.TotalItemCount;

            return searchResult;
        }

        /// <summary>
        /// Get the results for the given query
        /// </summary>
        public IEnumerable<T> Search<T>(IBooleanOperation query, out int totalResults)
            where T : class, IPublishedContent
        {
            var searchResult = query.Execute();

            totalResults = (int)searchResult.TotalItemCount;

            var typedResult = searchResult.GetResults<T>();

            return typedResult;
        }

        /// <summary>
        /// Get paged results for the given query
        /// </summary>
        public IEnumerable<ISearchResult> Page(IBooleanOperation query, int page, int perPage, out int totalResults)
        {
            var searchResult = Search(query, out totalResults);

            var pagedResult = searchResult
                .Skip((page - 1) * perPage)
                .Take(perPage);

            return pagedResult;
        }

        /// <summary>
        /// Get paged results for the given query
        /// </summary>
        public IEnumerable<T> Page<T>(IBooleanOperation query, int page, int perPage, out int totalResults)
            where T : class, IPublishedContent
        {
            var searchResult = Search(query, out totalResults);

            var pagedResult = searchResult
                .Skip((page - 1) * perPage)
                .GetResults<T>()
                .Take(perPage);

            return pagedResult;
        }
    }
}