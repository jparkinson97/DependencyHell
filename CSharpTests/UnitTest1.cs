using DependencyHell.ClassDependencies;
using DependencyHell.ExternalInterfaces.DTOs;
using DependencyHell.ExternalInterfaces.Examples;
using DependencyHell.General;
using System.Reflection;

namespace CSharpTests
{
    public class UnitTest1
    {
        [Fact]
        public void CheckFileDependenciesGrabExplicitReferences()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            var referencedAssemblies = FileDependencyExtensions.GetReferencedAssemblies(currentAssembly);
            var test = "";
        }

        [Fact]
        public void BuildDependencyTree()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            var referencedAssemblies = FileDependencyExtensions.GetReferencedAssemblies(currentAssembly);

            var dependencyTree = DependencyTreeBuilder.BuildDependencyTree(referencedAssemblies, []);
            var typesInTree = dependencyTree.SelectMany(n => n.Value.GetMemberTypes());

            Assert.NotEmpty(typesInTree);
        }

        [Fact]
        public void BuildDependencyTreeWithTotalFilterCreatesEmptyTree()
        {
            TotalFilter filter = new();
            NoFilter filter2 = new();
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            var referencedAssemblies = FileDependencyExtensions.GetReferencedAssemblies(currentAssembly);

            var dependencyTree = DependencyTreeBuilder.BuildDependencyTree(referencedAssemblies, [filter]);
            var typesInTree = dependencyTree.SelectMany(n => n.Value.GetMemberTypes());
            var assemblies = dependencyTree.Values.ToList();
            Assert.Empty(typesInTree);
        }

        [Fact]
        public void GrabTypesReferencedInAssembly()
        {
            GraphData graphData = new GraphData();
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            var referencedAssemblies = FileDependencyExtensions.GetReferencedTypes(currentAssembly);
            var test = "";
        }
    }
}