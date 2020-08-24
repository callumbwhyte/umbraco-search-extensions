using System;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using UdiEntityType = Umbraco.Core.Constants.UdiEntityType;

namespace Our.Umbraco.Extensions.Search.Helpers
{
    internal class PublishedContentHelper
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;

        public PublishedContentHelper(IUmbracoContextFactory umbracoContextFactory)
        {
            _umbracoContextFactory = umbracoContextFactory;
        }

        public IPublishedContent GetByString(string id)
        {
            if (int.TryParse(id, out int intId) == true)
            {
                return GetByInt(intId);
            }

            if (Guid.TryParse(id, out Guid guidId) == true)
            {
                return GetByGuid(guidId);
            }

            if (Udi.TryParse(id, out Udi udi) == true)
            {
                return GetByUdi(udi);
            }

            return null;
        }

        public IPublishedContent GetByInt(int id)
        {
            using (var context = _umbracoContextFactory.EnsureUmbracoContext())
            {
                return context.UmbracoContext.Content.GetById(id)
                    ?? context.UmbracoContext.Media.GetById(id);
            }
        }

        public IPublishedContent GetByGuid(Guid id)
        {
            using (var context = _umbracoContextFactory.EnsureUmbracoContext())
            {
                return context.UmbracoContext.Content.GetById(id)
                    ?? context.UmbracoContext.Media.GetById(id);
            }
        }

        public IPublishedContent GetByUdi(Udi udi)
        {
            using (var context = _umbracoContextFactory.EnsureUmbracoContext())
            {
                var umbracoType = UdiEntityType.ToUmbracoObjectType(udi.EntityType);

                if (umbracoType == UmbracoObjectTypes.Document)
                {
                    return context.UmbracoContext.Content.GetById(udi);
                }

                if (umbracoType == UmbracoObjectTypes.Media)
                {
                    return context.UmbracoContext.Media.GetById(udi);
                }
            }

            return null;
        }
    }
}