using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyHell.General
{
    public class AssemblyNode
    {
        private readonly Assembly _assembly;
        private readonly Dictionary<string, TypeNode> _memberTypes = [];
        public readonly string Version;

        public AssemblyNode(Assembly assembly)
        {
            _assembly = assembly;
            Version = assembly.GetName().Version!.ToString(); // what do I reference here?
        }

        public void AddMemberTypes(List<TypeNode> types)
        {
            foreach (var type in types)
            {
                if (type != null)
                {
                    _memberTypes[type.Type.FullName!] = type;
                }
            }
        }

        public TypeNode GetTypeNode(string typeFullName)
        {
            return _memberTypes[typeFullName];
        }

        public string GetAssemblyFullName()
        {
            return _assembly.FullName!;
        }

        public List<TypeNode> GetMemberTypes()
        {
            return _memberTypes.Values.ToList();
        }

        public List<string> GetDependentAssemblies()
        {
            List<string> dependentAssemblies = [];
            foreach (var type in _memberTypes)
            {
                foreach (var dependentType in type.Value.GetDependents())
                {
                    dependentAssemblies
                        .Add(
                            dependentType
                            .GetAssemblyFullName()
                            );
                }
            }

            return dependentAssemblies;
        }
    }
}
