using System.IO;
using Lucene.Net.Analysis;

namespace Our.Umbraco.Extensions.Search.LuceneEngine.Analysis
{
    internal class WhitespaceSeparatorTokenizer : WhitespaceTokenizer
    {
        public WhitespaceSeparatorTokenizer(TextReader @in, char separator)
            : base(@in)
        {
            Separator = separator;
        }

        public char Separator { get; }

        protected override bool IsTokenChar(char c)
        {
            return base.IsTokenChar(c) && Separator.Equals(c) == false;
        }
    }
}