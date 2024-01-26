using Pool;
using UnityEngine;

public class UnitFactory
{
    private readonly PoolInstantiateObject<UnitBase> _poolUnit;
    private readonly Transform _content;

    public UnitFactory(PoolInstantiateObject<UnitBase> poolUnit, Transform content)
    {
        _content = content;
        _poolUnit = poolUnit;
    }

    public (UnitBase, bool) Spawn(Vector3 position)
    {
        var obj = _poolUnit.GetInstantiate();
        if (obj.Item1 != null)
        {
            var transform = obj.Item1.transform;
            transform.parent = _content;
            transform.position = position;
        }
        return obj;
    }
}