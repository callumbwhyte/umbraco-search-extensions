using Microsoft.Extensions.DependencyInjection;
using Our.Umbraco.Extensions.Search.Helpers;
using Our.Umbraco.Extensions.Search.Mappers;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

#if NET8_0_OR_GREATER
    using UmbracoDependencyInjection13 = Umbraco.Cms.Core.DependencyInjection;
#else
    using UmbracoDependencyInjection = Umbraco.Cms.Web.Common.DependencyInjection;
#endif

namespace Our.Umbraco.Extensions.Search.Composing
{
    public class SearchComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddSingleton<SearchHelper>();

            builder.Services.AddSingleton<PublishedContentHelper>();

            builder.Services.ConfigureOptions<ConfigureIndexOptions>();

            builder.MapDefinitions().Add<PublishedContentMapper>();

#if NET8_0_OR_GREATER
            ServiceLocator.Configure(type => UmbracoDependencyInjection13.StaticServiceProvider.Instance.GetService(type));
#else
            ServiceLocator.Configure(type => UmbracoDependencyInjection.StaticServiceProvider.Instance.GetService(type));
#endif
        }
    }
}