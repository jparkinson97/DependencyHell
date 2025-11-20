
namespace DependencyHell.General
{
    public class TypeNode
    {
        private readonly AssemblyNode _assemblyNode;
        public Type Type {  get; }
        private List<TypeNode> dependents = [];

        public TypeNode(AssemblyNode assemblyNode, Type type)
        {
            _assemblyNode = assemblyNode;
            Type = type;
        }

        public AssemblyNode GetAssembly()
        {
            return _assemblyNode;
        }
        public List<TypeNode> GetDependents()
        {
            return dependents;
        }

        public void AddDependents(List<TypeNode> typeNodes)
        {
            dependents.AddRange(typeNodes);
        }

        public void AddDependent(TypeNode typeNode)
        {
            
            dependents.Add(typeNode);
            
        }
    }
}
