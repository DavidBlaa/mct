namespace MCT.DB.Services
{
    public interface IManager<T,V>  where T: new()
                                        where V: struct
    {
        T Create<T>(T t);
        T[] GetAll<T>();
        T Get(V v);
        void Update(T t);
    }
}
