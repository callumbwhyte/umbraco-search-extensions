using System.Collections.Generic;
using System.Linq;
using Examine.LuceneEngine.Indexing;
using Lucene.Net.Documents;

namespace Our.Umbraco.Extensions.Search.LuceneEngine.ValueTypes
{
    internal class MultiValueType : IndexFieldValueTypeBase
    {
        private readonly IEnumerable<IIndexFieldValueType> _fieldTypes;

        public MultiValueType(string fieldName, IEnumerable<IIndexFieldValueType> fieldTypes)
            : base(fieldName)
        {
            _fieldTypes = fieldTypes;
        }

        protected override void AddSingleValue(Document doc, object value)
        {
            foreach (var fieldType in _fieldTypes)
            {
                var fields = doc.GetFields(fieldType.FieldName);

                if (fields.Any() == true)
                {
                    doc.RemoveFields(fieldType.FieldName);

                    foreach (var field in fields)
                    {
                        fieldType.AddValue(doc, field.StringValue);
                    }
                }
                else
                {
                    fieldType.AddValue(doc, value);
                }
            }
        }
    }
}