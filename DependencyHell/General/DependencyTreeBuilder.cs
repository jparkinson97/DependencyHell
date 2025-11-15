using DependencyHell.ClassDependencies;
using System.Reflection;

namespace DependencyHell.General
{
    internal class DependencyTreeBuilder
    {
        public Dictionary<string, AssemblyNode> BuildDependencyTree(List<Assembly> assemblies)
        {
            Dictionary<string, AssemblyNode> assemblyNodes = [];

            foreach (var  assembly in assemblies)
            {
                assemblyNodes[assembly.FullName!] = assembly.ToAssemblyNode();
            }

            foreach (var assembly in assemblies)
            {
                // list types
                // foreach type
                // find the types it references
                // find that type in the assemblyNodes dictionary
                // add itself to the dependents 
            }
        }

    }
}
