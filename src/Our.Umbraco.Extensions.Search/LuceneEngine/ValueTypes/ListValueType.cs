using Examine.LuceneEngine.Indexing;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Our.Umbraco.Extensions.Search.LuceneEngine.Analysis;

namespace Our.Umbraco.Extensions.Search.LuceneEngine.ValueTypes
{
    public class ListValueType : IndexFieldValueTypeBase
    {
        public ListValueType(string fieldName, char separator = ',')
            : base(fieldName)
        {
            Separator = separator;
        }

        public char Separator { get; }

        protected override void AddSingleValue(Document doc, object value)
        {
            if (value is string stringValue)
            {
                doc.Add(new Field(FieldName, stringValue, Field.Store.YES, Field.Index.ANALYZED));
            }
        }

        public override void SetupAnalyzers(PerFieldAnalyzerWrapper analyzer)
        {
            analyzer.AddAnalyzer(FieldName, new WhitespaceSeparatorAnalyzer(Separator));
        }
    }
}