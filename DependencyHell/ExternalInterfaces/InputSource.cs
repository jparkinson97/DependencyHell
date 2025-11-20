using DependencyHell.General;

namespace DependencyHell.ExternalInterfaces
{
    public class InputSource // Extend this class to send changes
    {
        private TypeUpdateTracker _updateTracker;
        public InputSource(TypeUpdateTracker updateTracker)
        {
            _updateTracker = updateTracker;
        }

        public void Add(TypeNode type)
        {
            _updateTracker.AddTypeUpdate(type);
        }
    }
}
