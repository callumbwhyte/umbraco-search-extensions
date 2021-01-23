using Examine.Search;

namespace Our.Umbraco.Extensions.Search
{
    public static class NestedQueryExtensions
    {
        /// <summary>
        /// Query documents with the specified template ID assigned
        /// </summary>
        /// <remarks>
        /// If no <paramref name="templateId"/> is given, queries for documents without a template ID assigned
        /// </remarks>
        public static INestedBooleanOperation HasTemplate(this INestedQuery query, int templateId = 0)
        {
            return query.Field("templateID", templateId.ToString());
        }

        /// <summary>
        /// Query documents marked as published
        /// </summary>
        public static INestedBooleanOperation IsPublished(this INestedQuery query)
        {
            return query.Field("__Published", "y");
        }

        /// <summary>
        /// Query documents marked as visible
        /// </summary>
        /// <remarks>
        /// A document is marked as visible when <c>umbracoNaviHide</c> is set to <c>false</c>
        /// </remarks>
        public static INestedBooleanOperation IsVisble(this INestedQuery query)
        {
            return query.Field("umbracoNaviHide", "0");
        }

        /// <summary>
        /// Query documents with any of the specified node type aliases
        /// </summary>
        public static INestedBooleanOperation NodeTypeAlias(this INestedQuery query, string[] aliases)
        {
            return query.GroupedOr(new[] { "__NodeTypeAlias" }, aliases);
        }
    }
}