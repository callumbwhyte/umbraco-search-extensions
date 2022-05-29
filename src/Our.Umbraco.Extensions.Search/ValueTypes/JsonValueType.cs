using System.Collections.Generic;
using Examine.Lucene.Indexing;
using Lucene.Net.Documents;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Our.Umbraco.Extensions.Search.ValueTypes
{
    public class JsonValueType : IndexFieldValueTypeBase
    {
        public JsonValueType(string fieldName, ILoggerFactory loggerFactory, string path = "")
            : base(fieldName, loggerFactory)
        {
            Path = path;
        }

        public string Path { get; }

        protected override void AddSingleValue(Document doc, object value)
        {
            if (value is string valueString)
            {
                var json = JToken.Parse(valueString);

                var tokens = json.SelectTokens(Path);

                foreach (var token in tokens)
                {
                    var fields = GetFields(token, FieldName);

                    foreach (var field in fields)
                    {
                        doc.Add(new TextField(field.Key, field.Value, Field.Store.YES));
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
            else
            {
                values.Add(new KeyValuePair<string, string>(alias, token.ToObject<string>()));
            }

            return values;
        }
    }
}