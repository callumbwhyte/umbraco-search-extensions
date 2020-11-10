using Examine.LuceneEngine.Indexing;
using Our.Umbraco.Extensions.Search.LuceneEngine.Analysis;

namespace Our.Umbraco.Extensions.Search.LuceneEngine.ValueTypes
{
    public class ListValueType : FullTextType
    {
        public ListValueType(string fieldName)
            : base(fieldName, new WhitespaceSeparatorAnalyzer(','))
        {

        }
    }
}