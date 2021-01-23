using System.Collections.Generic;
using System.Linq;
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


        #region Cultures

        /// <summary>
        /// Query documents with the specified name and culture
        /// </summary>
        public static INestedBooleanOperation NodeName(this INestedQuery query, string nodeName, string culture)
        {
            return query.Field("nodeName", culture, nodeName);
        }

        /// <summary>
        /// Query documents with the specified field and culture
        /// </summary>
        public static INestedBooleanOperation Field<T>(this INestedQuery query, string fieldName, string fieldCulture, T fieldValue)
            where T : struct
        {
            var cultureField = GetFieldName(fieldName, fieldCulture);

            return query.Field<T>(cultureField, fieldValue);
        }

        /// <summary>
        /// Query documents with the specified field and culture
        /// </summary>
        public static INestedBooleanOperation Field(this INestedQuery query, string fieldName, string fieldCulture, string fieldValue)
        {
            var cultureField = GetFieldName(fieldName, fieldCulture);

            return query.Field(cultureField, fieldValue);
        }

        /// <summary>
        /// Query documents with the specified field and culture
        /// </summary>
        public static INestedBooleanOperation Field(this INestedQuery query, string fieldName, string fieldCulture, IExamineValue fieldValue)
        {
            var cultureField = GetFieldName(fieldName, fieldCulture);

            return query.Field(cultureField, fieldValue);
        }

        /// <summary>
        /// Query documents with all of the specified fields and culture
        /// </summary>
        public static INestedBooleanOperation GroupedAnd(this INestedQuery query, IEnumerable<string> fields, string fieldCulture, params string[] fieldValues)
        {
            var culturedFields = fields.Select(x => GetFieldName(x, fieldCulture));

            return query.GroupedAnd(culturedFields, fieldValues);
        }

        /// <summary>
        /// Query documents with all of the specified fields and culture
        /// </summary>
        public static INestedBooleanOperation GroupedAnd(this INestedQuery query, IEnumerable<string> fields, string fieldCulture, params IExamineValue[] fieldValues)
        {
            var culturedFields = fields.Select(x => GetFieldName(x, fieldCulture));

            return query.GroupedAnd(culturedFields, fieldValues);
        }

        /// <summary>
        /// Query documents with any of the specified fields and culture
        /// </summary>
        public static INestedBooleanOperation GroupedOr(this INestedQuery query, IEnumerable<string> fields, string fieldCulture, params string[] fieldValues)
        {
            var culturedFields = fields.Select(x => GetFieldName(x, fieldCulture));

            return query.GroupedOr(culturedFields, fieldValues);
        }

        /// <summary>
        /// Query documents with any of the specified fields and culture
        /// </summary>
        public static INestedBooleanOperation GroupedOr(this INestedQuery query, IEnumerable<string> fields, string fieldCulture, params IExamineValue[] fieldValues)
        {
            var culturedFields = fields.Select(x => GetFieldName(x, fieldCulture));

            return query.GroupedOr(culturedFields, fieldValues);
        }

        /// <summary>
        /// Query documents without any of the specified fields and culture
        /// </summary>
        public static INestedBooleanOperation GroupedNot(this INestedQuery query, IEnumerable<string> fields, string fieldCulture, params string[] fieldValues)
        {
            var culturedFields = fields.Select(x => GetFieldName(x, fieldCulture));

            return query.GroupedNot(culturedFields, fieldValues);
        }

        /// <summary>
        /// Query documents without any of the specified fields and culture
        /// </summary>
        public static INestedBooleanOperation GroupedNot(this INestedQuery query, IEnumerable<string> fields, string fieldCulture, params IExamineValue[] fieldValues)
        {
            var culturedFields = fields.Select(x => GetFieldName(x, fieldCulture));

            return query.GroupedNot(culturedFields, fieldValues);
        }

        private static string GetFieldName(string fieldName, string culture)
        {
            if (string.IsNullOrWhiteSpace(culture) == false)
            {
                return fieldName + "_" + culture.ToLower();
            }

            return fieldName;
        }

        #endregion
    }
}