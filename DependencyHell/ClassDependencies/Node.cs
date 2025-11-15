using DependencyHell.General;
using System.Reflection.Metadata.Ecma335;

namespace DependencyHell.ClassDependencies
{
    public class Node : INode
    {
        public string Name { get; set; }
        public Type UnderlyingType { get; set; }
        private List<IConnection> connections { get; set; }

        public List<IConnection> Connections => connections;

        public List<IConnection> GetDependees()
        {
            throw new NotImplementedException();
        }

        public void AddConnection(IConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}
