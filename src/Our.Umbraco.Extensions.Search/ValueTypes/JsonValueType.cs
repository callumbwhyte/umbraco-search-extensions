using System.Collections.Generic;
using Examine.Lucene.Indexing;
using Lucene.Net.Documents;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Our.Umbraco.Extensions.Search.ValueTypes
{
    public class JsonValueType : IndexFieldValueTypeBase
    {
        public JsonValueType(string fieldName, ILoggerFactory loggerFactory, bool store = true)
            : base(fieldName, loggerFactory, store)
        {

        }

        public string Path { get; init; } = string.Empty;

        protected override void AddSingleValue(Document doc, object value)
        {
            if (value is string stringValue)
            {
                var json = JToken.Parse(stringValue);

                var tokens = json.SelectTokens(Path);

                foreach (var token in tokens)
                {
                    var fields = GetFields(token, FieldName);

                    foreach (var field in fields)
                    {
                        doc.Add(new TextField(field.Key, field.Value, Store ? Field.Store.YES : Field.Store.NO));
                    }
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
            else if (token.ToObject<string>() is string value)
            {
                values.Add(new KeyValuePair<string, string>(alias, value));
            }

            return values;
        }
    }
}