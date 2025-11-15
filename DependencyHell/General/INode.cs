
namespace DependencyHell.General
{
    public interface INode
    {
        string Name { get; }
        List<IConnection> Connections { get; }
        List<IConnection> GetDependees();
        void AddConnection(IConnection connection);
    }
}
