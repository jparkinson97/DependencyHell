
namespace DependencyHell.General
{
    public class TypeUpdateTracker : IObservable<TypeNode>
    {
        public TypeUpdateTracker()
        {
            observers = new List<IObserver<TypeNode>>();
        }

        private List<IObserver<TypeNode>> observers;

        public IDisposable Subscribe(IObserver<TypeNode> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<TypeNode>> _observers;
            private IObserver<TypeNode> _observer;

            public Unsubscriber(List<IObserver<TypeNode>> observers, IObserver<TypeNode> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public void AddTypeUpdate(TypeNode typeNode)
        {
            foreach (var observer in observers)
            {

                observer.OnNext(typeNode);
            }
        }

        public void EndTransmission()
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();

            observers.Clear();
        }
    }
}
