
using DependencyHell.ExternalInterfaces;
using DependencyHell.ExternalInterfaces.DTOs;
using System.Collections.Generic;
using System.Reflection;

namespace DependencyHell.General
{
    public interface IDependencyManager
    {
        void Initialise();
        void AddActor(IDependentObserver actor);
    }

    public class DependencyManager
    {
        private readonly TypeUpdateTracker _typeUpdateTracker;
        private Dictionary<string, AssemblyNode> dependencyTree; // maybe we can update this later, doesnt have to be readonly 
        private List<Assembly> assemblies;
        private List<ITypeFilter> filters = [];

        public DependencyManager(List<Assembly> assemblies)
        {
            _typeUpdateTracker = new();
            this.assemblies = assemblies;
        }

        public void Initialise() // this needs to change. write an add assembly option to the dependencytreebuilder
        {
            BuildDependencyTree();
        }

        public void AddFilter(ITypeFilter filter)
        {
            filters.Add(filter);
        }

        public void AddActor(IDependentObserver actor)
        {
            _typeUpdateTracker.Subscribe(actor);
        }

        private void BuildDependencyTree()
        {
            dependencyTree = DependencyTreeBuilder.BuildDependencyTree(assemblies, filters);
        }

        public void AddUpdate(TypeDTO typeTdo)
        {
            foreach (var affectedDependent in FindDepedenents(typeTdo))
            {
                _typeUpdateTracker.AddTypeUpdate(affectedDependent);
            }
        }

        public List<TypeNode> FindDepedenents(TypeDTO typeDto) // There needs to be a distiction between the type incoming and the internal type 
        {
            var assembly = dependencyTree[typeDto.AssemblyFullName];
            var type = assembly.GetTypeNode(typeDto.TypeFullName);

            return type.GetDependents();
        }
        // on input, 
        // find dependent types
        // send those to the tracker.

    }
}
