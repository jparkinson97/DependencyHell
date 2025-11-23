using DependencyHell.ClassDependencies;
using DependencyHell.ExternalInterfaces;
using System.Collections.Generic;
using System.Reflection;

namespace DependencyHell.General
{
    public static class DependencyTreeBuilder
    {
        public static Dictionary<string, AssemblyNode> BuildDependencyTree(List<Assembly> assemblies, List<ITypeFilter> typeFilters)
        {
            Dictionary<string, AssemblyNode> assemblyNodes = [];

            foreach (var assembly in assemblies)
            {
                assemblyNodes[assembly.FullName!] = assembly.ToAssemblyNode(typeFilters);
            }

            // Need to build the data structure first. Might be possible to do all in one shot but id give up a few ms runtime for clarity.
            // Dont like the depth of nesting. Could wrap in Linq when logic tested.
            foreach (var assembly in assemblyNodes)
            {
                foreach (var  type in assembly.Value.GetMemberTypes())
                {
                    foreach (var referencedType in FileDependencyExtensions.GetReferencedTypes(type.Type))
                    {
                        var assemblyName = referencedType.Assembly.FullName!;
                        if (assemblyNodes.TryGetValue(assemblyName, out var referencedAssembly))
                        {
                            if (referencedType.FullName != null)
                            {
                                referencedAssembly
                                .GetTypeNode(referencedType.FullName)
                                .AddDependent(type);
                            }
                        }
                    }
                }
            }

            return assemblyNodes;
        }
    }
}