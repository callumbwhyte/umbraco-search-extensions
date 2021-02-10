using System.IO;
using Lucene.Net.Analysis;

namespace Our.Umbraco.Extensions.Search.LuceneEngine.Analysis
{
    public class WhitespaceSeparatorAnalyzer : Analyzer
    {
        private char Separator { get; }

        public WhitespaceSeparatorAnalyzer(char separator)
        {
            Separator = separator;
        }

        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            return new WhitespaceSeparatorTokenizer(reader, Separator);
        }

        public override TokenStream ReusableTokenStream(string fieldName, TextReader reader)
        {
            var tokenizer = (Tokenizer)PreviousTokenStream;

            if (tokenizer == null)
            {
                tokenizer = new WhitespaceSeparatorTokenizer(reader, Separator);

                PreviousTokenStream = tokenizer;
            }
            else
            {
                tokenizer.Reset(reader);
            }

            return tokenizer;
        }
    }
}