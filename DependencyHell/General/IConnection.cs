
namespace DependencyHell.General
{
    public interface IConnection
    {
        public string Dependee {  get; }
        public string Depender { get; }
    }
}
