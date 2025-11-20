
using DependencyHell.ExternalInterfaces;
using System.Reflection;

namespace DependencyHell.General
{
    public class DependencyManager
    {
        private readonly TypeUpdateTracker _typeUpdateTracker;
        private Dictionary<string, AssemblyNode> dependencyTree; // maybe we can update this later, doesnt have to be readonly 
        private List<Assembly> assemblies;

        public DependencyManager(List<Assembly> assemblies)
        {
            _typeUpdateTracker = new();
            this.assemblies = assemblies;
        }

        public void Initialise() // this needs to change. write an add assembly option to the dependencytreebuilder
        {
            BuildDependencyTree();
        }

        public void AddActor(IActor actor)
        {
            _typeUpdateTracker.Subscribe(actor);
        }

        public void BuildDependencyTree()
        {
            dependencyTree = DependencyTreeBuilder.BuildDependencyTree(assemblies);
        }
    }
}
