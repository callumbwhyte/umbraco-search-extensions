using System;
using System.Collections.Generic;
using Examine.Lucene;
using Examine.Lucene.Indexing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Our.Umbraco.Extensions.Search.ValueTypes;
#if NET8_0_OR_GREATER
using Umbraco.Cms.Core.DependencyInjection;
#endif
#if !NET8_0_OR_GREATER
using Umbraco.Cms.Web.Common.DependencyInjection;
#endif

namespace Our.Umbraco.Extensions.Search.Factories
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
            var loggerFactory = StaticServiceProvider.Instance.GetRequiredService<ILoggerFactory>();

            return new MultiValueType(fieldName, loggerFactory, _factories(fieldName));
        }
    }
}