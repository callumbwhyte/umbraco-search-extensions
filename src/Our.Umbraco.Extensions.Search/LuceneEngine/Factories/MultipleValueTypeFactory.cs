using System;
using System.Collections.Generic;
using Examine.LuceneEngine;
using Examine.LuceneEngine.Indexing;
using Our.Umbraco.Extensions.Search.LuceneEngine.ValueTypes;

namespace Our.Umbraco.Extensions.Search.LuceneEngine.Factories
{
    public class MultipleValueTypeFactory : IFieldValueTypeFactory
    {
        private readonly Func<string, IEnumerable<IIndexFieldValueType>> _factories;

        public MultipleValueTypeFactory(Func<string, IEnumerable<IIndexFieldValueType>> factories)
        {
            _factories = factories;
        }

        public IIndexFieldValueType Create(string fieldName)
        {
            return new MultiValueType(fieldName, _factories(fieldName));
        }
    }
}