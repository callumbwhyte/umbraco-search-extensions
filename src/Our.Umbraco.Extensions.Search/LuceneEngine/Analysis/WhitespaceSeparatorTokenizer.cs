using System.IO;
using Lucene.Net.Analysis;

namespace Our.Umbraco.Extensions.Search.LuceneEngine.Analysis
{
    public class WhitespaceSeparatorTokenizer : WhitespaceTokenizer
    {
        public WhitespaceSeparatorTokenizer(TextReader @in, string separator)
            : base(@in)
        {
            Separator = separator;
        }

        public string Separator { get; }

        protected override bool IsTokenChar(char c)
        {
            return base.IsTokenChar(c) && Separator.Equals(c) == false;
        }
    }
}