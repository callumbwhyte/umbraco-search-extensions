using System.Collections.Generic;
using Examine;
using Examine.Lucene;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Our.Umbraco.Extensions.Search.ValueTypes;

namespace Our.Umbraco.Extensions.Search.Composing
{
    internal class ConfigureIndexOptions : IConfigureNamedOptions<LuceneDirectoryIndexOptions>
    {
        private readonly ILoggerFactory _loggerFactory;

        public ConfigureIndexOptions(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public void Configure(string name, LuceneDirectoryIndexOptions options)
        {
            options.IndexValueTypesFactory = new Dictionary<string, IFieldValueTypeFactory>
            {
                { "json", new DelegateFieldValueTypeFactory(fieldName => new JsonValueType(fieldName, _loggerFactory)) },
                { "list", new DelegateFieldValueTypeFactory(fieldName => new ListValueType(fieldName, _loggerFactory)) },
                { "picker", new DelegateFieldValueTypeFactory(fieldName => new PickerValueType(fieldName, _loggerFactory)) },
                { "udi", new DelegateFieldValueTypeFactory(fieldName => new UdiValueType(fieldName, _loggerFactory)) },
            };

            options.FieldDefinitions.AddOrUpdate(new FieldDefinition("path", "list"));

            options.FieldDefinitions.AddOrUpdate(new FieldDefinition("createDate", "date"));

            options.FieldDefinitions.AddOrUpdate(new FieldDefinition("updateDate", "date"));
        }

        public void Configure(LuceneDirectoryIndexOptions options) => Configure(string.Empty, options);
    }
}