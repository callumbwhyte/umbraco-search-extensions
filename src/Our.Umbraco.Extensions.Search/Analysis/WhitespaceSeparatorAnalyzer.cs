using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Util;

namespace Our.Umbraco.Extensions.Search.Analysis
{
    public class WhitespaceSeparatorAnalyzer : Analyzer
    {
        public WhitespaceSeparatorAnalyzer(LuceneVersion matchVersion, char separator)
        {
            MatchVersion = matchVersion;
            Separator = separator;
        }

        public LuceneVersion MatchVersion { get; }

        public char Separator { get; }

        protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
        {
            return new TokenStreamComponents(new WhitespaceSeparatorTokenizer(MatchVersion, reader, Separator));
        }
    }
}