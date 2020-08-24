using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Our.Umbraco.Extensions.Search.Startup
{
    public class IndexComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<IndexComponent>();
        }
    }
}