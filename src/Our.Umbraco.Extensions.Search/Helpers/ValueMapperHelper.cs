using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
#if NET8_0_OR_GREATER
using Umbraco.Cms.Core.DependencyInjection;
#endif
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Models.PublishedContent;
#if !NET8_0_OR_GREATER
using Umbraco.Cms.Web.Common.DependencyInjection;
#endif

namespace Our.Umbraco.Extensions.Search.Helpers
{
    internal class ValueMapperHelper
    {
        private readonly IUmbracoMapper _mapper;

        public ValueMapperHelper(IUmbracoMapper mapper)
        {
            _mapper = mapper;
        }

        public static ValueMapperHelper Instance => StaticServiceProvider.Instance.GetRequiredService<ValueMapperHelper>();

        public T ConvertValue<T>(object value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            if (converter.CanConvertFrom(value.GetType()) == true)
            {
                return (T)converter.ConvertFrom(value);
            }

            if (typeof(IPublishedContent).IsAssignableFrom(typeof(T)) == true)
            {
                return (T)_mapper.Map<IPublishedContent>(value);
            }

            return _mapper.Map<T>(value);
        }
    }
}