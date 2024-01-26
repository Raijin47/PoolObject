using System.Collections;
using Pool;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private UnitLocator _unitLocator;
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private float _intervalSpawn;
    [SerializeField] private float _distance;
    [SerializeField] private float _speed;

    private Timer _timer;
    private UnitProvider _unitProvider;
    private PoolInstantiateObject<UnitBase> _instantiateObject;
    private Coroutine _spawnCoroutine;

    public float IntervalSpawn => _intervalSpawn;
    public float Distance => _distance;
    public float Speed => _speed;

    public void Init()
    {
        _instantiateObject = new PoolInstantiateObject<UnitBase>(_unitLocator.UnitBase);
        _unitProvider = new UnitProvider(_instantiateObject, _content);
        _timer = new Timer(_intervalSpawn);
        StartSpawn();
    }

    IEnumerator SpawnProcces()
    {
        while (true)
        {
            _timer.Update();
            if(_timer.IsCompleted)
            {
                Spawn(_spawnPoint.position);
                _timer.RestartTimer();
            }
            yield return null;
        }
    }
    private void StartSpawn()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
        _spawnCoroutine = StartCoroutine(SpawnProcces());
    }

    public void Spawn(Vector3 spawnPosition)
    {
        _unitProvider.CreateUnit(spawnPosition, _speed, _distance);
    }
}