using Lucene.Net.Documents;
using Our.Umbraco.Extensions.Search.Helpers;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Our.Umbraco.Extensions.Search.LuceneEngine.ValueTypes
{
    public class PickerValueType : ListValueType
    {
        private readonly PublishedContentHelper _publishedContentHelper;

        private readonly string _aliasPrefix;

        public PickerValueType(string fieldName, string aliasPrefix = Constants.SearchPrefix)
            : base(fieldName)
        {
            _publishedContentHelper = Current.Factory.GetInstance<PublishedContentHelper>();

            _aliasPrefix = aliasPrefix;
        }

        protected override void AddSingleValue(Document doc, object value)
        {
            base.AddSingleValue(doc, value);

            if (value is string valueString)
            {
                var ids = valueString.Split(',');

                foreach (var id in ids)
                {
                    if (Udi.TryParse(id, out Udi udi) == true)
                    {
                        var content = _publishedContentHelper.GetByUdi(udi);

                        if (content != null)
                        {
                            if (content.UrlSegment != null)
                            {
                                doc.Add(new Field(_aliasPrefix + FieldName, content.UrlSegment, Field.Store.YES, Field.Index.ANALYZED));
                            }
                        }
                    }
                }
            }
        }
    }
}