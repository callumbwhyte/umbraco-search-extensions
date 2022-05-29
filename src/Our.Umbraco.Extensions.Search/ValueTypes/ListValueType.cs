using Examine.Lucene.Indexing;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Util;
using Microsoft.Extensions.Logging;
using Our.Umbraco.Extensions.Search.Analysis;

namespace Our.Umbraco.Extensions.Search.ValueTypes
{
    public class ListValueType : IndexFieldValueTypeBase
    {
        public ListValueType(string fieldName, ILoggerFactory loggerFactory, char separator = ',')
            : base(fieldName, loggerFactory)
        {
            Separator = separator;
        }

        public char Separator { get; }

        protected override void AddSingleValue(Document doc, object value)
        {
            if (value is string stringValue)
            {
                doc.Add(new TextField(FieldName, stringValue, Field.Store.YES));
            }
        }

        public override Analyzer Analyzer => new WhitespaceSeparatorAnalyzer(LuceneVersion.LUCENE_CURRENT, Separator);
    }
}