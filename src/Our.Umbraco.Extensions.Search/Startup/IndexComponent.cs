using Examine;
using Examine.LuceneEngine;
using Our.Umbraco.Extensions.Search.LuceneEngine.ValueTypes;
using Umbraco.Core.Composing;
using Umbraco.Examine;

namespace Our.Umbraco.Extensions.Search.Startup
{
    public class IndexComponent : IComponent
    {
        private readonly IExamineManager _examineManager;

        public IndexComponent(IExamineManager examineManager)
        {
            _examineManager = examineManager;
        }

        public void Initialize()
        {
            foreach (var index in _examineManager.Indexes)
            {
                if (!(index is UmbracoExamineIndex umbracoIndex))
                {
                    continue;
                }

                var fieldDefinitions = umbracoIndex.FieldDefinitionCollection;

                var valueTypeFactories = umbracoIndex.FieldValueTypeCollection.ValueTypeFactories;

                valueTypeFactories.AddOrUpdate("json", new DelegateFieldValueTypeFactory(x => new JsonValueType(x)));

                valueTypeFactories.AddOrUpdate("list", new DelegateFieldValueTypeFactory(x => new ListValueType(x)));

                valueTypeFactories.AddOrUpdate("picker", new DelegateFieldValueTypeFactory(x => new PickerValueType(x)));

                valueTypeFactories.AddOrUpdate("udi", new DelegateFieldValueTypeFactory(x => new UdiValueType(x)));

                fieldDefinitions.AddOrUpdate(new FieldDefinition("path", "list"));

                fieldDefinitions.AddOrUpdate(new FieldDefinition("createDate", "date"));

                fieldDefinitions.AddOrUpdate(new FieldDefinition("updateDate", "date"));
            }
        }

        public void Terminate()
        {

        }
    }
}