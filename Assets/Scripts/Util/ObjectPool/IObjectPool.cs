namespace Util.ObjectPool
{
    public interface IObjectPool
    {
        object Get();
        void Return(object item);
    }
    
    public interface IObjectPool<T> : IObjectPool 
    {
        new T Get();
        void Return(T item);
    }
}
