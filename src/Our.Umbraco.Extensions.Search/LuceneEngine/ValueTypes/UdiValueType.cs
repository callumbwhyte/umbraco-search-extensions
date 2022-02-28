using System;
using Examine.LuceneEngine.Indexing;
using Lucene.Net.Documents;
using Umbraco.Core;

namespace Our.Umbraco.Extensions.Search.LuceneEngine.ValueTypes
{
    public class UdiValueType : IndexFieldValueTypeBase
    {
        public UdiValueType(string fieldName, char separator = ',')
            : base(fieldName)
        {
            Separator = separator;
        }

        public char Separator { get; }

        protected override void AddSingleValue(Document doc, object value)
        {
            if (value is string valueString)
            {
                var ids = valueString.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var id in ids)
                {
                    if (GuidUdi.TryParse(id, out GuidUdi udi) == true)
                    {
                        doc.Add(new Field(FieldName, udi.Guid.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    }
                }
            }
        }
    }
}