
using DependencyHell.General;

namespace DependencyHell.ExternalInterfaces
{
    public interface ITypeFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool Filter(TypeNode type);
    }
}
