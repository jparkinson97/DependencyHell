
namespace DependencyHell.General
{
    public class GraphBuilder
    {
        // We wont consider versioning for now 
        public Dictionary<string, INode> RegisterDependencies(Dictionary<string, INode> nodes)
        {
            foreach (INode node in nodes.Values)
            {
                foreach (var dependency in node.GetDependees())
                {
                    if (nodes.TryGetValue(dependency.Dependee, out INode? dependee))
                    {
                        dependee.AddConnection(dependency);
                    }
                }
            }

            return nodes;
        }
    }
}
