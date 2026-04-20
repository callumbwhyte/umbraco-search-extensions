using System;
using Examine.Lucene.Indexing;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Documents;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;

namespace Our.Umbraco.Extensions.Search.ValueTypes
{
    public class UdiValueType : IndexFieldValueTypeBase
    {
        public UdiValueType(string fieldName, ILoggerFactory loggerFactory, char separator = ',', bool store = true)
            : base(fieldName, loggerFactory, store)
        {
            Separator = separator;
        }

        public char Separator { get; }

        public override Analyzer? Analyzer => new KeywordAnalyzer();

        protected override void AddSingleValue(Document doc, object value)
        {
            if (value is string valueString)
            {
                var ids = valueString.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var id in ids)
                {
                    if (UdiParser.TryParse(id, out GuidUdi udi) == true)
                    {
                        doc.Add(new StringField(FieldName, udi.Guid.ToString(), Store ? Field.Store.YES : Field.Store.NO));
                    }
                }
            }
        }
    }
}