using DependencyHell.General;
using System;
using System.Threading.Tasks;

namespace DependencyHell.ExternalInterfaces
{
    /// <summary>
    /// Completes an action if a dependent is updated by an upstream
    /// </summary>
    public interface IDependentObserver : IObserver<TypeNode>
    {
        Task Act(TypeNode node);
    }
}
