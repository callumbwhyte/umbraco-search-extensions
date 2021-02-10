using System;
using Lucene.Net.Documents;
using Umbraco.Core;

namespace Our.Umbraco.Extensions.Search.LuceneEngine.ValueTypes
{
    public class UdiValueType : ListValueType
    {
        private char Separator { get; }

        public UdiValueType(string fieldName, char separator = ',')
            : base(fieldName, separator)
        {
            Separator = separator;
        }

        protected override void AddSingleValue(Document doc, object value)
        {
            base.AddSingleValue(doc, value);

            if (value is string valueString)
            {
                var ids = valueString.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var id in ids)
                {
                    if (Udi.TryParse(id, out Udi udi) == true)
                    {
                        doc.Add(new Field(FieldName, udi.ToString(), Field.Store.NO, Field.Index.NOT_ANALYZED));
                    }
                }
            }
        }
    }
}