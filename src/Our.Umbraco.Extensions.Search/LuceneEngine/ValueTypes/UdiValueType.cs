using System;
using Examine.LuceneEngine.Indexing;
using Lucene.Net.Documents;
using Umbraco.Core;

namespace Our.Umbraco.Extensions.Search.LuceneEngine.ValueTypes
{
    public class UdiValueType : FullTextType
    {
        public UdiValueType(string fieldName)
            : base(fieldName)
        {

        }

        protected override void AddSingleValue(Document doc, object value)
        {
            if (value is string valueString)
            {
                var ids = valueString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var id in ids)
                {
                    if (Udi.TryParse(id, out Udi udi) == true)
                    {
                        doc.Add(new Field(FieldName, udi.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    }
                }
            }
        }
    }
}