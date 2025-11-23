using DependencyHell.ExternalInterfaces;
using DependencyHell.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CSharpTests
{
    internal class TestClass
    {
        JsonArray test = new JsonArray();
    }

    public class TotalFilter : ITypeFilter
    {
        public bool Filter(TypeNode type)
        {
            return false;
        }
    }
}
