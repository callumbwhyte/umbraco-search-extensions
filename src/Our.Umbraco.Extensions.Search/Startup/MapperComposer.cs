using Our.Umbraco.Extensions.Search.Helpers;
using Our.Umbraco.Extensions.Search.Mappers;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Mapping;

namespace Our.Umbraco.Extensions.Search.Startup
{
    public class MapperComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.RegisterUnique<PublishedContentHelper>();

            composition
                .WithCollectionBuilder<MapDefinitionCollectionBuilder>()
                .Add<PublishedContentMapper>();
        }
    }
}