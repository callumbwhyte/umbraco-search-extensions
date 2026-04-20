using Examine;
using Examine.Lucene.Indexing;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Microsoft.Extensions.Logging;
using Our.Umbraco.Extensions.Search.Analysis;

namespace Our.Umbraco.Extensions.Search.ValueTypes
{
    public class ListValueType : IndexFieldValueTypeBase
    {
        public ListValueType(string fieldName, ILoggerFactory loggerFactory, char separator = ',', bool store = true)
            : base(fieldName, loggerFactory, store)
        {
            Separator = separator;
        }

        public char Separator { get; }

        public override Analyzer Analyzer => new WhitespaceSeparatorAnalyzer(LuceneInfo.CurrentVersion, Separator);

        protected override void AddSingleValue(Document doc, object value)
        {
            if (value is string stringValue)
            {
                doc.Add(new TextField(FieldName, stringValue, Store ? Field.Store.YES : Field.Store.NO));
            }
        }
    }
}