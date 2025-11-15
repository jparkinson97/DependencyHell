namespace DependencyHell.ClassDependencies
{
    public class PackageReference(string packageName, string packageVersion)
    {
        public string PackageName { get; } = packageName;
        public string PackageVersion { get; } = packageVersion;
    }
}
