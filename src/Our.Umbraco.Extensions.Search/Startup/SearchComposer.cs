using Our.Umbraco.Extensions.Search.Helpers;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Our.Umbraco.Extensions.Search.Startup
{
    public class SearchComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.RegisterUnique<SearchHelper>();
        }
    }
}