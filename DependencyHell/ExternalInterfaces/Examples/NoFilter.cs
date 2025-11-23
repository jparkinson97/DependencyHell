using DependencyHell.General;

namespace DependencyHell.ExternalInterfaces.Examples
{
    public class NoFilter : ITypeFilter
    {
        public bool Filter(TypeNode type)
        {
            return true;
        }
    }
}
