using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class PoolInstantiateObject<T> where T : Object, IPoolObject<T>
    {
        private List<T> _usedPoolInstantiate = new List<T>();
        private List<T> _freePoolInstantiate = new List<T>();

        public T _data;

        public PoolInstantiateObject(T data)
        {
            _data = data;
        }

        public T Instantiate()
        {
            return Object.Instantiate(_data);
        }

        public (T, bool) GetInstantiate()
        {
            if (_freePoolInstantiate.Count <= 0)
            {
                var element = Instantiate();
                _usedPoolInstantiate.Add(element);
                return (element, true);
            }
            var elementGet = _freePoolInstantiate[_freePoolInstantiate.Count - 1];
            _freePoolInstantiate.Remove(elementGet);
            _usedPoolInstantiate.Add(elementGet);
            return (elementGet, false);
        }

        public void Release(T data)
        {
            _usedPoolInstantiate.Remove(data);
            _freePoolInstantiate.Add(data);
        }
    }
}