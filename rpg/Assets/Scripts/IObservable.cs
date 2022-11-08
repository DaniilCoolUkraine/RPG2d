using JetBrains.Annotations;

public interface IObservable
{
    public void Subscribe(IObserver observer);
    public void Unsubscribe(IObserver observer);
}
