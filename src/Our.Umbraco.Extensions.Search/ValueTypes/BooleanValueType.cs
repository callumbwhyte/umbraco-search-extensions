using Examine.Lucene.Indexing;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Microsoft.Extensions.Logging;
using Umbraco.Extensions;

namespace Our.Umbraco.Extensions.Search.ValueTypes
{
    public class BooleanValueType : IndexFieldValueTypeBase, IIndexRangeValueType<bool>, IIndexRangeValueType<int>
    {
        public BooleanValueType(string fieldName, ILoggerFactory loggerFactory, bool store = true)
            : base(fieldName, loggerFactory, store)
        {

        }

        protected override void AddSingleValue(Document doc, object value)
        {
            var boolValue = ConvertValue(value);

            doc.Add(new Int32Field(FieldName, boolValue ? 1 : 0, Store ? Field.Store.YES : Field.Store.NO));
        }

        public override Query GetQuery(string value)
        {
            return base.GetQuery(ConvertValue(value) ? "1" : "0");
        }

        public Query GetQuery(bool? lower, bool? upper, bool lowerInclusive = true, bool upperInclusive = true)
        {
            return GetQuery(lower == true ? 1 : 0, upper == true ? 1 : 0, lowerInclusive, upperInclusive);
        }

        public Query GetQuery(int? lower, int? upper, bool lowerInclusive = true, bool upperInclusive = true)
        {
            return NumericRangeQuery.NewInt32Range(FieldName, lower, upper, lowerInclusive, upperInclusive);
        }

        private bool ConvertValue(object value)
        {
            if (value is bool boolValue)
            {
                return boolValue;
            }
            else if (value is int intValue)
            {
                return intValue == 1;
            }
            else if (value is long longValue)
            {
                return longValue == 1;
            }
            else if (value is string stringValue)
            {
                return stringValue.InvariantEquals("true")
                    || stringValue.InvariantEquals("1")
                    || stringValue.InvariantEquals("yes")
                    || stringValue.InvariantEquals("y");
            }

            bool.TryParse(value?.ToString(), out boolValue);

            return boolValue;
        }
    }
}