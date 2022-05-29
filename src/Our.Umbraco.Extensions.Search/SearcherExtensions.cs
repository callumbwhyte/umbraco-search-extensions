using Examine;
using Examine.Search;

namespace Our.Umbraco.Extensions.Search
{
    public static class SearcherExtensions
    {
        /// <summary>
        /// Creates a query with predefined criteria for published content
        /// </summary>
        public static IBooleanOperation CreatePublishedQuery(this ISearcher searcher, string category = null, BooleanOperation defaultOperation = BooleanOperation.And)
        {
            var query = searcher
                .CreateQuery(category, defaultOperation)
                .IsPublished();

            query.And().IsVisible();

            query.And().HasTemplate();

            return query;
        }
    }
}