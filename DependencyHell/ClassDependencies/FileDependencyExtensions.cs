using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace DependencyHell.ClassDependencies
{
    public static class FileDependencyExtensions
    {
        public static List<PackageReference> GetPackagereferences(string csproj)
        {
            string regexPattern = @"<(PackageReference|ProjectReference)\s+Include=""(?<name>[^""]+)""(?:.*?\s+Version=""(?<version>[^""]+)"")?.*?\/>";
            var regex = new Regex(regexPattern);
            var matches = regex.Matches(csproj);

            List<PackageReference> result = [];

            foreach (Match package in matches)
            {
                char[] charsToTrim = { '{', ' ', '<', '>', '}' };
                var value = package.Value.Trim(charsToTrim);
                var packageString = value;
                var versionString = string.Empty;
                var versionValue = string.Empty;

                if (value.Contains("Version"))
                {
                    var split = value.Split("Version");
                    packageString = split[0];
                    versionString = split[1];

                    versionValue = versionString.Split('=')[1].Trim('\"');
                }
                var packageValue = packageString.Split('=')[1].Trim('\"');

                result.Add(new(packageValue, versionValue));
            }

            return result;
        }

        public static IEnumerable<Type> GetReferencedTypes(Assembly assembly)
        {
            List<Type> referencedTypes = [];
            var types = GetClasses(assembly);
            foreach (Type type in types)
            {
                referencedTypes.AddRange(GetReferencedTypes(type));
            }

            return referencedTypes;
        }

        public static Assembly GetAssembly(AssemblyName assemblyName)
        {
            return Assembly.Load(assemblyName);
        }

        public static AssemblyName[] GetReferencedAssemblies(Assembly assembly)
        {
            return assembly.GetReferencedAssemblies();
        }

        public static Type[] GetClasses(Assembly assembly)
        {
            return [.. assembly.GetTypes().Where(type => type.IsClass)];
        }

        public static IEnumerable<Type> GetReferencedTypes(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var referencedTypes = new HashSet<Type>();

            if (type.BaseType != null)
            {
                referencedTypes.Add(type.BaseType);
            }

            foreach (var implementedInterface in type.GetInterfaces())
            {
                referencedTypes.Add(implementedInterface);
            }

            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                referencedTypes.Add(field.FieldType);
            }

            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                referencedTypes.Add(prop.PropertyType);
            }

            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                referencedTypes.Add(method.ReturnType);

                foreach (var param in method.GetParameters())
                {
                    referencedTypes.Add(param.ParameterType);
                }
            }

            foreach (var attribute in type.GetCustomAttributes(false))
            {
                referencedTypes.Add(attribute.GetType());
            }

            var typesToInspectForGenerics = new Queue<Type>(referencedTypes);
            while (typesToInspectForGenerics.Count > 0)
            {
                var currentType = typesToInspectForGenerics.Dequeue();

                if (currentType.IsGenericType)
                {
                    foreach (var genericArg in currentType.GetGenericArguments())
                    {
                        if (referencedTypes.Add(genericArg))
                        {
                            typesToInspectForGenerics.Enqueue(genericArg);
                        }
                    }
                }

                if (currentType.IsArray)
                {
                    var elementType = currentType.GetElementType();
                    if (referencedTypes.Add(elementType))
                    {
                        typesToInspectForGenerics.Enqueue(elementType);
                    }
                }
            }

            referencedTypes.Remove(type);

            return referencedTypes;
        }
    }
}
