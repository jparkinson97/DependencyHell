using System.Reflection;

namespace DependencyHell.General
{
    public class AssemblyNode
    {
        private readonly Assembly _assembly;
        private readonly List<TypeNode> _types;

        public AssemblyNode(Assembly assembly)
        {
            _assembly = assembly;
        }

        public void AddTypes(List<TypeNode> types)
        {
            _types.AddRange(types);
        }

        public string GetAssemblyName()
        {
            return _assembly.FullName!;
        }

        public List<string> GetDependentAssemblies()
        {
            List<string> dependentAssemblies = [];
            foreach (var type in _types)
            {
                foreach (var dependentType in type.GetDependents())
                {
                    dependentAssemblies
                        .Add(
                            dependentType
                            .GetAssembly()
                            .GetAssemblyName()
                            );
                }
            }

            return dependentAssemblies;
        }
    }
}
