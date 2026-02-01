using System;
using Lucene.Net.Documents;
using Microsoft.Extensions.Logging;
using Our.Umbraco.Extensions.Search.Helpers;

namespace Our.Umbraco.Extensions.Search.ValueTypes
{
    public class PickerValueType : UdiValueType
    {
        public PickerValueType(string fieldName, ILoggerFactory loggerFactory, bool store = true)
            : base(fieldName, loggerFactory, store)
        {

        }

        protected override void AddSingleValue(Document doc, object value)
        {
            base.AddSingleValue(doc, value);

            if (value is string stringValue)
            {
                var ids = stringValue.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                foreach (var id in ids)
                {
                    var content = PublishedContentHelper.Instance.GetByString(id);

                    if (content?.UrlSegment != null)
                    {
                        doc.Add(new StringField(FieldName, content.UrlSegment, Field.Store.NO));
                    }
                }
            }
        }
    }
}