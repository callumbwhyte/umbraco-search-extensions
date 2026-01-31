using System.ComponentModel;
using Our.Umbraco.Extensions.Search.Composing;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Our.Umbraco.Extensions.Search.Helpers
{
    internal class ValueMapperHelper
    {
        private readonly IUmbracoMapper _mapper;

        public ValueMapperHelper(IUmbracoMapper mapper)
        {
            _mapper = mapper;
        }

        public static ValueMapperHelper Instance => ServiceLocator.GetInstance<ValueMapperHelper>();

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