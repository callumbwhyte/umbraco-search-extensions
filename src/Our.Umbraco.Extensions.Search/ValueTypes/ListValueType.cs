using System;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Documents;
using Lucene.Net.Util;
using Microsoft.Extensions.Logging;

namespace Our.Umbraco.Extensions.Search.ValueTypes
{
    public class ListValueType : IndexFacetFieldValueType
    {
        public ListValueType(string fieldName, ILoggerFactory loggerFactory, bool store = true, bool faceted = false, bool taxonomyFaceted = false)
            : base(fieldName, loggerFactory, store, faceted, taxonomyFaceted)
        {

        }

        public string[] Separators { get; init; } = [",", "\r\n", "\n"];

        public override Analyzer? Analyzer => new WhitespaceAnalyzer(LuceneVersion.LUCENE_48);

        protected override void AddSingleValue(Document doc, object value)
        {
            if (value is string stringValue)
            {
                var items = stringValue.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in items)
                {
                    doc.Add(new TextField(FieldName, item, Store ? Field.Store.YES : Field.Store.NO));

                    AddSingleFacetValue(doc, item);
                }
            }
        }
    }
}