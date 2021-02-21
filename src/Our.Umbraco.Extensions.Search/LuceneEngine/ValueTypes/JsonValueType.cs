using System.Collections.Generic;
using Examine.LuceneEngine.Indexing;
using Lucene.Net.Documents;
using Newtonsoft.Json.Linq;

namespace Our.Umbraco.Extensions.Search.LuceneEngine.ValueTypes
{
    public class JsonValueType : IndexFieldValueTypeBase
    {
        public JsonValueType(string fieldName)
            : base(fieldName)
        {

        }

        protected override void AddSingleValue(Document doc, object value)
        {
            if (value is string valueString)
            {
                var json = JToken.Parse(valueString);

                var fields = GetFields(json, FieldName);

                foreach (var field in fields)
                {
                    doc.Add(new Field(field.Key, field.Value, Field.Store.YES, Field.Index.ANALYZED));
                }
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetFields(JToken token, string alias)
        {
            var values = new List<KeyValuePair<string, string>>();

            if (token is JProperty property)
            {
                alias = alias + "_" + property.Name;
            }

            if (token.HasValues == true)
            {
                foreach (var child in token.Children())
                {
                    values.AddRange(GetFields(child, alias));
                }
            }
            else
            {
                values.Add(new KeyValuePair<string, string>(alias, token.ToObject<string>()));
            }

            return values;
        }
    }
}