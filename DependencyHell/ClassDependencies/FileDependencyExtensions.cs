using DependencyHell.General;
using System.Reflection;
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
            foreach (Type type in GetClasses(assembly))
            {
                referencedTypes.AddRange(GetReferencedTypes(type));
            }

            return referencedTypes;
        }

        public static List<Assembly> GetReferencedAssemblies(Assembly assembly)
        {
            List<Assembly> referencedAssemblies = [];
            foreach (var assemblyName in assembly.GetReferencedAssemblies())
            {
                referencedAssemblies.Add(Assembly.Load(assemblyName));
            }

            return referencedAssemblies;
        }

        public static AssemblyNode ToAssemblyNode(this Assembly assembly)
        {
            AssemblyNode assemblyNode = new(assembly);

            List<TypeNode> typeNodes = [];
            foreach (var type in assembly.GetTypes())
            {
                typeNodes.Add(new(assemblyNode, type));
            }

            assemblyNode.AddMemberTypes(typeNodes);

            return assemblyNode;
        }

        public static Type[] GetClasses(Assembly assembly)
        {

            var types = assembly.GetTypes();
            return types.ToArray();
        }

        public static IEnumerable<Type> GetReferencedTypes(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var finalReferencedTypes = new HashSet<Type>();

            var directReferences = new HashSet<Type>();
            if (type.BaseType != null) directReferences.Add(type.BaseType);
            foreach (var i in type.GetInterfaces()) directReferences.Add(i);
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)) directReferences.Add(field.FieldType);
            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)) directReferences.Add(prop.PropertyType);
            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                directReferences.Add(method.ReturnType);
                foreach (var param in method.GetParameters()) directReferences.Add(param.ParameterType);
            }
            foreach (var attribute in type.GetCustomAttributes(false)) directReferences.Add(attribute.GetType());

            foreach (var refType in directReferences)
            {
                foreach (var definitionType in GetUnderlyingTypeDefinitions(refType))
                {
                    finalReferencedTypes.Add(definitionType);
                }
            }

            finalReferencedTypes.Remove(type);

            return finalReferencedTypes;
        }

        private static IEnumerable<Type> GetUnderlyingTypeDefinitions(Type type)
        {
            var queue = new Queue<Type>();
            queue.Enqueue(type);

            while (queue.Count > 0)
            {
                Type currentType = queue.Dequeue();
                if (currentType == null) continue;

                if (currentType.HasElementType)
                {
                    queue.Enqueue(currentType.GetElementType());
                    continue;
                }

                if (currentType.IsGenericType)
                {
                    yield return currentType.GetGenericTypeDefinition();

                    foreach (var genericArg in currentType.GetGenericArguments())
                    {
                        if (!genericArg.IsGenericParameter) // Filter out placeholder types like 'T'
                        {
                            queue.Enqueue(genericArg);
                        }
                    }
                    continue;
                }

                if (!currentType.IsGenericParameter)
                {
                    yield return currentType;
                }
            }
        }
    }
}
