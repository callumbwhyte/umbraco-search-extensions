using System;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

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

            if (UdiParser.TryParse(id, out Udi udi) == true)
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
                var umbracoType = UdiEntityTypeHelper.ToUmbracoObjectType(udi.EntityType);

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