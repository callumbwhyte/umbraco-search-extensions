using System.Collections.Generic;
using System.Linq;
using Examine;
using Examine.Search;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.PublishedCache;

namespace Our.Umbraco.Extensions.Search.Helpers
{
    public class SearchHelper
    {
        private readonly IExamineManager _examineManager;
        private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;

        public SearchHelper(IExamineManager examineManager, IPublishedSnapshotAccessor publishedSnapshotAccessor)
        {
            _examineManager = examineManager;
            _publishedSnapshotAccessor = publishedSnapshotAccessor;
        }

        /// <summary>
        /// Get a searcher by name
        /// </summary>
        public ISearcher GetSearcher(string name)
        {
            _examineManager.TryGetSearcher(name, out ISearcher searcher);

            return searcher;
        }

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

            var typedResult = searchResult
                .ToPublishedSearchResults(_publishedSnapshotAccessor.PublishedSnapshot.Content)
                .Select(x => x.Content as T)
                .Where(x => x != null);

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
                .ToPublishedSearchResults(_publishedSnapshotAccessor.PublishedSnapshot.Content)
                .Select(x => x.Content as T)
                .Where(x => x != null)
                .Take(perPage);

            return pagedResult;
        }
    }
}