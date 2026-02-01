using System;
using Examine.Lucene.Indexing;
using Lucene.Net.Documents;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;

namespace Our.Umbraco.Extensions.Search.ValueTypes
{
    public class UdiValueType : IndexFieldValueTypeBase
    {
        public UdiValueType(string fieldName, ILoggerFactory loggerFactory, bool store = true)
            : base(fieldName, loggerFactory, store)
        {

        }

        public string[] Separators { get; init; } = [",", "\r\n", "\n"];

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
                    }
                }
            }
        }
    }
}