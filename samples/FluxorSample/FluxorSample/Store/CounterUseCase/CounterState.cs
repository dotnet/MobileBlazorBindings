using Fluxor;

namespace FluxorSample.Store.CounterUseCase
{
    [FeatureState]
    public class CounterState
    {
        public int ClickCount { get; }

        private CounterState() { }
        public CounterState(int clickCount)
        {
            ClickCount = clickCount;
        }
    }
}
