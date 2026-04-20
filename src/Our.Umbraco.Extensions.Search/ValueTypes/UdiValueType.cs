using System;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Documents;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;

namespace Our.Umbraco.Extensions.Search.ValueTypes
{
    public class UdiValueType : IndexFacetFieldValueType
    {
        public UdiValueType(string fieldName, ILoggerFactory loggerFactory, bool store = true, bool faceted = false, bool taxonomyFaceted = false)
            : base(fieldName, loggerFactory, store, faceted, taxonomyFaceted)
        {

        }

        public string[] Separators { get; init; } = [",", "\r\n", "\n"];

        public override Analyzer? Analyzer => new KeywordAnalyzer();

        protected override void AddSingleValue(Document doc, object value)
        {
            if (value is string stringValue)
            {
                var ids = stringValue.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                foreach (var id in ids)
                {
                    if (UdiParser.TryParse(id, out GuidUdi? udi) && udi != null)
                    {
                        doc.Add(new StringField(FieldName, udi.Guid.ToString(), Store ? Field.Store.YES : Field.Store.NO));

                        AddSingleFacetValue(doc, udi.Guid.ToString());
                    }
                }
            }
        }
    }
}