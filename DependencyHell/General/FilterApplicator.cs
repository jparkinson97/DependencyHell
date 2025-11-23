using DependencyHell.ExternalInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace DependencyHell.General
{
    public static class FilterApplicator
    {
        public static List<TypeNode> ApplyFilters(this List<TypeNode> types, List<ITypeFilter> filters)
        {
            foreach (var filter in filters)
            {
                types = types
                    .Where(t => filter
                    .Filter(t))
                    .ToList();
            }

            return types;
        }
    }
}
