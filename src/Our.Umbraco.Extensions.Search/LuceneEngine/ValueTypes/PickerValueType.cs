using System;
using Lucene.Net.Documents;
using Our.Umbraco.Extensions.Search.Helpers;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Our.Umbraco.Extensions.Search.LuceneEngine.ValueTypes
{
    public class PickerValueType : ListValueType
    {
        private readonly PublishedContentHelper _publishedContentHelper;

        public PickerValueType(string fieldName)
            : base(fieldName, ',')
        {
            _publishedContentHelper = Current.Factory.GetInstance<PublishedContentHelper>();
        }

        protected override void AddSingleValue(Document doc, object value)
        {
            base.AddSingleValue(doc, value);

            if (value is string valueString)
            {
                var ids = valueString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var id in ids)
                {
                    var content = _publishedContentHelper.GetByString(id);

                    if (content != null)
                    {
                        if (content.UrlSegment != null)
                        {
                            doc.Add(new Field(FieldName, content.UrlSegment, Field.Store.YES, Field.Index.NOT_ANALYZED));
                        }
                    }
                }
            }
        }
    }
}