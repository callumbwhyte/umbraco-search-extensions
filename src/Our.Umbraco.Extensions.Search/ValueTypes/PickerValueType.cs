using System;
using Lucene.Net.Documents;
using Microsoft.Extensions.Logging;
using Our.Umbraco.Extensions.Search.Composing;
using Our.Umbraco.Extensions.Search.Helpers;

namespace Our.Umbraco.Extensions.Search.ValueTypes
{
    public class PickerValueType : UdiValueType
    {
        private readonly PublishedContentHelper _publishedContentHelper;

        public PickerValueType(string fieldName, ILoggerFactory loggerFactory, char separator = ',')
            : base(fieldName, loggerFactory, separator)
        {
            _publishedContentHelper = ServiceLocator.GetInstance<PublishedContentHelper>();
        }

        protected override void AddSingleValue(Document doc, object value)
        {
            base.AddSingleValue(doc, value);

            if (value is string valueString)
            {
                var ids = valueString.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var id in ids)
                {
                    var content = _publishedContentHelper.GetByString(id);

                    if (content != null)
                    {
                        if (content.UrlSegment != null)
                        {
                            doc.Add(new StringField(FieldName, content.UrlSegment, Field.Store.NO));
                        }
                    }
                }
            }
        }
    }
}