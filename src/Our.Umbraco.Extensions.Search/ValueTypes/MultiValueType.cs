using System.Collections.Generic;
using System.Linq;
using Examine.Lucene.Indexing;
using Lucene.Net.Documents;
using Microsoft.Extensions.Logging;

namespace Our.Umbraco.Extensions.Search.ValueTypes
{
    internal class MultiValueType : IndexFieldValueTypeBase
    {
        private readonly IEnumerable<IIndexFieldValueType> _fieldTypes;

        public MultiValueType(string fieldName, ILoggerFactory loggerFactory, IEnumerable<IIndexFieldValueType> fieldTypes)
            : base(fieldName, loggerFactory)
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
                        fieldType.AddValue(doc, field.GetStringValue());
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