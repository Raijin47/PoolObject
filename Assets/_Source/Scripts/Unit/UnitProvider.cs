using System.Collections.Generic;
using Pool;
using UnityEngine;

public class UnitProvider
{
    private List<UnitBase> _unitBases = new List<UnitBase>();
    private readonly PoolInstantiateObject<UnitBase> _poolInstantiateObject;
    private readonly UnitFactory _unitfactory;

    public UnitProvider(PoolInstantiateObject<UnitBase> poolInstantiateObject, Transform _content)
    {
        _unitfactory = new UnitFactory(poolInstantiateObject, _content);
        _poolInstantiateObject = poolInstantiateObject;
    }

    private void InitUnit(UnitBase unitBase, float speed, float distance, Vector3 position)
    {
        unitBase.gameObject.SetActive(true);
        unitBase.Die += OnDie;
        unitBase.Init(speed, distance, position);
    }

    private void OnDie(UnitBase unitBase)
    {
        Remove(unitBase);
    }

    public void CreateUnit(Vector3 position, float speed, float distance)
    {
        var unitData = _unitfactory.Spawn(position);
        var unitBase = unitData.Item1;
        if (unitBase == null)
            return;
        var isInstantiate = unitData.Item2;
        if (isInstantiate) Add(unitBase, speed, distance, position);
        else ResetPoolUnit(unitBase);
    }

    private void ResetPoolUnit(UnitBase unitBase)
    {
        unitBase.gameObject.SetActive(true);
        unitBase.ResetData();
    }

    public void Add(UnitBase unitBase, float speed, float distance, Vector3 position)
    {
        _unitBases.Add(unitBase);
        InitUnit(unitBase, speed, distance, position);
    }

    public void Remove(UnitBase unitBase)
    {
        unitBase.gameObject.SetActive(false);
        _unitBases.Remove(unitBase);
        unitBase.Release();
        _poolInstantiateObject.Release(unitBase);
    }
}