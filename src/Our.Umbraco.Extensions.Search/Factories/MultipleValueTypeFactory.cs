using System;
using System.Collections.Generic;
using Examine.Lucene;
using Examine.Lucene.Indexing;
using Microsoft.Extensions.Logging;
using Our.Umbraco.Extensions.Search.ValueTypes;

namespace Our.Umbraco.Extensions.Search.Factories
{
    public class MultipleValueTypeFactory : IFieldValueTypeFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly Func<string, IEnumerable<IIndexFieldValueType>> _factories;

        public MultipleValueTypeFactory(ILoggerFactory loggerFactory, Func<string, IEnumerable<IIndexFieldValueType>> factories)
        {
            _loggerFactory = loggerFactory;
            _factories = factories;
        }

        public IIndexFieldValueType Create(string fieldName)
        {
            return new MultiValueType(fieldName, _loggerFactory, _factories(fieldName));
        }
    }
}