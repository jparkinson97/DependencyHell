using DependencyHell.ClassDependencies;
using DependencyHell.General;
using System.Reflection;

namespace CSharpTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var PackageReferences = FileDependencyExtensions.GetPackagereferences("<Project Sdk=\"Microsoft.NET.Sdk\">\r\n\r\n  <PropertyGroup>\r\n    <OutputType>Exe</OutputType>\r\n    <TargetFramework>net8.0</TargetFramework>\r\n    <ImplicitUsings>enable</ImplicitUsings>\r\n    <Nullable>enable</Nullable>\r\n    <DockerDefaultTargetOS>Linux</DockerDefaultTargetOS>\r\n  </PropertyGroup>\r\n\r\n  <ItemGroup>\r\n    <PackageReference Include=\"Microsoft.VisualStudio.Azure.Containers.Tools.Targets\" Version=\"1.21.0\" />\r\n  </ItemGroup>\r\n\r\n</Project>\r\n");
            var test = "";
        }

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

            var dependencyTree = DependencyTreeBuilder.BuildDependencyTree(referencedAssemblies);
            var test = "";
        }

        [Fact]
        public void GrabTypesReferencedInAssembly()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            var referencedAssemblies = FileDependencyExtensions.GetReferencedTypes(currentAssembly);
            var test = "";
        }
    }
}