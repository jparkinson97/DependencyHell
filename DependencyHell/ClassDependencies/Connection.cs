
using DependencyHell.General;

namespace DependencyHell.ClassDependencies
{
    public class Connection : IConnection
    {
        public string Version;
        public Node Dependant;

        public string Dependee => throw new NotImplementedException();

        public string Depender => throw new NotImplementedException();
    }
}
