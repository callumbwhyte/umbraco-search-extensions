using System;
using System.Linq;
using Examine;
using Examine.Search;

namespace Our.Umbraco.Extensions.Search
{
    public static class SearchExtensions
    {
        /// <summary>
        /// Boosts the relevance of terms in search
        /// </summary>
        public static IExamineValue[] Boost(this string[] input, float boost)
        {
            return input
                .Select(x => x.Boost(boost))
                .ToArray();
        }

        /// <summary>
        /// Escapes the terms so they are used literally in search
        /// </summary>
        public static IExamineValue[] Escape(this string[] input)
        {
            return input
                .Select(x => x.Escape())
                .ToArray();
        }

        /// <summary>
        /// Fuzzy matches the terms in search
        /// </summary>
        public static IExamineValue[] Fuzzy(this string[] input, float fuzziness = 0.5f)
        {
            return input
                .Select(x => x.Fuzzy(fuzziness))
                .ToArray();
        }

        /// <summary>
        /// Matches terms with a single wildcard character in search
        /// </summary>
        public static IExamineValue[] SingleCharacterWildcard(this string[] input)
        {
            return input
                .Select(x => x.SingleCharacterWildcard())
                .ToArray();
        }

        /// <summary>
        /// Matches terms with a multiple wildcard characters in search
        /// </summary>
        public static IExamineValue[] MultipleCharacterWildcard(this string[] input)
        {
            return input
                .Select(x => x.MultipleCharacterWildcard())
                .ToArray();
        }

        /// <summary>
        /// Matches terms within the defined proximity of each other in search
        /// </summary>
        public static IExamineValue[] Proximity(this string[] input, int proximity)
        {
            return input
                .Select(x => x.Proximity(proximity))
                .ToArray();
        }

        /// <summary>
        /// Splits a string, removing any stop words and empty parts
        /// </summary>
        public static string[] ToSafeArray(this string input, string separator = " ")
        {
            return input
                .RemoveStopWords()
                .Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}