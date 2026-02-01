using System;
using Lucene.Net.Documents;
using Microsoft.Extensions.Logging;

namespace Our.Umbraco.Extensions.Search.ValueTypes
{
    public class ListValueType : IndexFieldValueTypeBase
    {
        public ListValueType(string fieldName, ILoggerFactory loggerFactory, bool store = true)
            : base(fieldName, loggerFactory, store)
        {

        }

        public string[] Separators { get; init; } = [",", "\r\n", "\n"];

        protected override void AddSingleValue(Document doc, object value)
        {
            if (value is string stringValue)
            {
                var items = stringValue.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in items)
                {
                    doc.Add(new TextField(FieldName, item, Store ? Field.Store.YES : Field.Store.NO));
                }
            }
        }
    }
}