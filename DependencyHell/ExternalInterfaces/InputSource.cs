
using DependencyHell.ExternalInterfaces.DTOs;
using System;

namespace DependencyHell.ExternalInterfaces
{
    public class InputSource // Extend this class to send changes
    {
        private readonly Action<TypeDTO> _addUpdatedType;
        public InputSource(Action<TypeDTO> addUpdatedType)
        {
            _addUpdatedType = addUpdatedType;
        }

        public void Add(TypeDTO type)
        {
            _addUpdatedType(type);
        }
    }
}
