using UnityEngine;

namespace Pool
{
    public interface IPoolObject<T> where T : Object
    {
        void Release();
    }
}