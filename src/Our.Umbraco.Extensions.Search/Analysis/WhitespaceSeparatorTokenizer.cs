using System.IO;
using J2N;
using Lucene.Net.Analysis.Util;
using Lucene.Net.Util;

namespace Our.Umbraco.Extensions.Search.Analysis
{
    internal sealed class WhitespaceSeparatorTokenizer : CharTokenizer
    {
        public WhitespaceSeparatorTokenizer(LuceneVersion matchVersion, TextReader input, char separator)
            : base(matchVersion, input)
        {
            Separator = separator;
        }

        public char Separator { get; }

        protected override bool IsTokenChar(int c)
        {
            return Character.IsWhiteSpace(c) == false && Separator.Equals((char)c) == false;
        }
    }
}