using System.Collections.Generic;
using Examine.Lucene.Indexing;
using Examine.Lucene.Search;
using Examine.Search;
using Lucene.Net.Documents;
using Lucene.Net.Facet;
using Lucene.Net.Facet.SortedSet;
using Microsoft.Extensions.Logging;

namespace Our.Umbraco.Extensions.Search.ValueTypes
{
    public abstract class IndexFacetFieldValueType : IndexFieldValueTypeBase, IIndexFacetValueType
    {
        public IndexFacetFieldValueType(string fieldName, ILoggerFactory loggerFactory, bool store = true, bool faceted = true, bool taxonomyFaceted = true)
            : base(fieldName, loggerFactory, store)
        {
            IsFaceted = faceted;
            IsTaxonomyFaceted = taxonomyFaceted;
        }

        public bool IsFaceted { get; }

        public bool IsTaxonomyFaceted { get; }

        public virtual void AddSingleFacetValue(Document doc, string value)
        {
            if (IsFaceted && IsTaxonomyFaceted)
            {
                doc.Add(new FacetField(FieldName, value));
            }
            else if (IsFaceted && !IsTaxonomyFaceted)
            {
                doc.Add(new SortedSetDocValuesFacetField(FieldName, value));
            }
        }

        public virtual IEnumerable<KeyValuePair<string, IFacetResult>> ExtractFacets(IFacetExtractionContext facetExtractionContext, IFacetField field)
        {
            return field.ExtractFacets(facetExtractionContext);
        }
    }
}