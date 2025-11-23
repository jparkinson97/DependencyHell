
namespace DependencyHell.ExternalInterfaces.DTOs
{
    public class GraphElement
    {
        public GraphData data { get; set; }
    }

    public class GraphData
    {
        public string id { get; set; }
        public string label { get; set; }
        public string parent { get; set; } // Links Types to Assemblies
        public string source { get; set; } // For edges
        public string target { get; set; } // For edges
        public string type { get; set; }   // "assembly" or "type"
    }
}
