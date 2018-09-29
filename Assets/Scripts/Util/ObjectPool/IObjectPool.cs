namespace Util.ObjectPool
{
    public interface IObjectPool<T>
    {
        T Get(float time = -1);
        void Return(T obj);
    }
}
