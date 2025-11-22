using Microsoft.Extensions.DependencyInjection;
using Our.Umbraco.Extensions.Search.Helpers;
using Our.Umbraco.Extensions.Search.Mappers;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
#if NET9_0_OR_GREATER
using Umbraco.Cms.Infrastructure.Examine;
#endif
#if !NET8_0_OR_GREATER
using Umbraco.Cms.Web.Common.DependencyInjection;
#endif

namespace Our.Umbraco.Extensions.Search.Composing
{
#if NET9_0_OR_GREATER
    [ComposeAfter(typeof(AddExamineComposer))]
#endif
    public class SearchComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddSingleton<SearchHelper>();

            builder.Services.AddSingleton<PublishedContentHelper>();

            builder.Services.ConfigureOptions<ConfigureIndexOptions>();

            builder.MapDefinitions().Add<PublishedContentMapper>();

            ServiceLocator.Configure(type => StaticServiceProvider.Instance.GetService(type));
        }
    }
}