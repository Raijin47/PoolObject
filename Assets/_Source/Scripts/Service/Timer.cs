using System;
using UnityEngine;

[Serializable]
public class Timer
{
    public bool IsCompleted = false;
    public float CurrentTime => _currentTime;
    public float RequiredTime
    {
        get => _requiredTime;
        set
        {
            _requiredTime = value;
        }
    }
    protected float _requiredTime = 0;
    protected float _currentTime = 0;

    public Timer(float timer)
    {
        _requiredTime = timer;
        _currentTime = _requiredTime;
    }

    public virtual void Update()
    {
        if (IsCompleted)
        {
            return;
        }

        if (_currentTime > 0f)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0f)
            {
                _currentTime = 0;
                IsCompleted = true;
            }
        }
    }

    public virtual void RestartTimer()
    {
        _currentTime = _requiredTime;
        IsCompleted = false;
    }
}