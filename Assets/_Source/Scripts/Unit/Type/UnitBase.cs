using System;
using System.Collections;
using Pool;
using UnityEngine;

public class UnitBase : MonoBehaviour, IPoolObject<UnitBase>
{
    public event Action<UnitBase> Die;

    [SerializeField] private Rigidbody _rigidbody;

    private WaitForSeconds _intervalCoroutine = new WaitForSeconds(0.2f);
    private Coroutine _checkDistanceCoroutine;
    private Coroutine _movementProcessCoroutine;
    private Vector3 _target;

    private float _moveSpeed;
    private float _moveDistance;
    private float _currentDistance;

    private bool _isActive;

    public void Init(float speed, float distance, Vector3 target)
    {
        _moveSpeed = speed;
        _moveDistance = distance;
        _target = target;

        ResetData();
    }
    private void Activate()
    {
        _isActive = true;
        if (_checkDistanceCoroutine != null)
        {
            StopCoroutine(_checkDistanceCoroutine);
            _checkDistanceCoroutine = null;
        }
        _checkDistanceCoroutine = StartCoroutine(UpdateCheckDistanceProcess());

        if (_movementProcessCoroutine != null)
        {
            StopCoroutine(_movementProcessCoroutine);
            _movementProcessCoroutine = null;
        }
        _movementProcessCoroutine = StartCoroutine(UpdateMovementProcess());
    }

    public void ResetData()
    {
        Activate();
    }

    private void ReturnToPool()
    {
        Deactivate();
        Die?.Invoke(this);
    }

    public void Release() { }

    private IEnumerator UpdateCheckDistanceProcess()
    {
        while (_isActive)
        {
            CheckDistance();
            yield return _intervalCoroutine;
        }
    }

    private void CheckDistance()
    {
        _currentDistance = Vector3.Distance(transform.position, _target);
        if (_currentDistance > _moveDistance) ReturnToPool();
    }

    private IEnumerator UpdateMovementProcess()
    {
        while (_isActive)
        {
            Move();
            yield return null;
        }
    }

    private void Move()
    {
        _rigidbody.velocity = Vector3.forward * _moveSpeed;
    }

    private void Deactivate()
    {
        _isActive = false;

        StopAllCoroutines();
        _checkDistanceCoroutine = null;
        _movementProcessCoroutine = null;
    }
}
