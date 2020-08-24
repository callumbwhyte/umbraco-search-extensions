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

                valueTypeFactories.AddOrUpdate("list", new DelegateFieldValueTypeFactory(x => new ListValueType(x)));

                fieldDefinitions.AddOrUpdate(new FieldDefinition("path", "list"));
            }
        }

        public void Terminate()
        {

        }
    }
}