using System.IO;
using Lucene.Net.Analysis;

namespace Our.Umbraco.Extensions.Search.LuceneEngine.Analysis
{
    public class WhitespaceSeparatorTokenizer : WhitespaceTokenizer
    {
        private char Separator { get; }

        public WhitespaceSeparatorTokenizer(TextReader @in, char separator)
            : base(@in)
        {
            Separator = separator;
        }

        protected override bool IsTokenChar(char c)
        {
            return base.IsTokenChar(c) && Separator.Equals(c) == false;
        }
    }
}