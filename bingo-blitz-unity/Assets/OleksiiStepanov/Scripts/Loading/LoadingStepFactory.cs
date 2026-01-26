using Zenject;

namespace BingoBlitzClone.Loading
{
    public class LoadingStepFactory
    {
        private readonly DiContainer _container;

        public LoadingStepFactory(DiContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T : LoadingStepBase
        {
            return _container.Instantiate<T>();
        }
    }
}