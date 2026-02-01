using System.Collections.Generic;
using System.Linq;
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

        public void Configure(string? name, LuceneDirectoryIndexOptions options)
        {
            var valueTypesFactory = options.IndexValueTypesFactory?.ToDictionary(x => x.Key, x => x.Value) ?? new Dictionary<string, IFieldValueTypeFactory>();

            // core value types

            valueTypesFactory.TryAdd("boolean", new DelegateFieldValueTypeFactory(fieldName => new BooleanValueType(fieldName, _loggerFactory)));

            valueTypesFactory.TryAdd("json", new DelegateFieldValueTypeFactory(fieldName => new JsonValueType(fieldName, _loggerFactory)));

            valueTypesFactory.TryAdd("list", new DelegateFieldValueTypeFactory(fieldName => new ListValueType(fieldName, _loggerFactory)));

            valueTypesFactory.TryAdd("picker", new DelegateFieldValueTypeFactory(fieldName => new PickerValueType(fieldName, _loggerFactory)));

            valueTypesFactory.TryAdd("udi", new DelegateFieldValueTypeFactory(fieldName => new UdiValueType(fieldName, _loggerFactory)));

            // facet value types

            valueTypesFactory.TryAdd("facetBoolean", new DelegateFieldValueTypeFactory(fieldName => new BooleanValueType(fieldName, _loggerFactory, faceted: true)));

            valueTypesFactory.TryAdd("facetJson", new DelegateFieldValueTypeFactory(fieldName => new JsonValueType(fieldName, _loggerFactory, faceted: true)));

            valueTypesFactory.TryAdd("facetList", new DelegateFieldValueTypeFactory(fieldName => new ListValueType(fieldName, _loggerFactory, faceted: true)));

            valueTypesFactory.TryAdd("facetPicker", new DelegateFieldValueTypeFactory(fieldName => new PickerValueType(fieldName, _loggerFactory, faceted: true)));

            valueTypesFactory.TryAdd("facetUdi", new DelegateFieldValueTypeFactory(fieldName => new UdiValueType(fieldName, _loggerFactory, faceted: true)));

            // facet taxonomy value types

            valueTypesFactory.TryAdd("facetTaxonomyList", new DelegateFieldValueTypeFactory(fieldName => new ListValueType(fieldName, _loggerFactory, faceted: true, taxonomyFaceted: true)));

            valueTypesFactory.TryAdd("facetTaxonomyPicker", new DelegateFieldValueTypeFactory(fieldName => new PickerValueType(fieldName, _loggerFactory, faceted: true, taxonomyFaceted: true)));

            valueTypesFactory.TryAdd("facetTaxonomyUdi", new DelegateFieldValueTypeFactory(fieldName => new UdiValueType(fieldName, _loggerFactory, faceted: true, taxonomyFaceted: true)));

            options.IndexValueTypesFactory = valueTypesFactory;

            options.FieldDefinitions.AddOrUpdate(new FieldDefinition("__Published", "boolean"));

            options.FieldDefinitions.AddOrUpdate(new FieldDefinition("__VariesByCulture", "boolean"));

            options.FieldDefinitions.AddOrUpdate(new FieldDefinition("path", "list"));

            options.FieldDefinitions.AddOrUpdate(new FieldDefinition("createDate", "date"));

            options.FieldDefinitions.AddOrUpdate(new FieldDefinition("updateDate", "date"));

            options.FieldDefinitions.AddOrUpdate(new FieldDefinition("umbracoNaviHide", "boolean"));
        }

        public void Configure(LuceneDirectoryIndexOptions options)
        {
            Configure(string.Empty, options);
        }
    }
}