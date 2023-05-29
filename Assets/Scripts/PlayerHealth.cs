using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _startHealth;
    [SerializeField] private UnityEvent _onHealthChange = new UnityEvent();

    public event UnityAction OnHealthChange
    {
        add => _onHealthChange.AddListener(value);
        remove => _onHealthChange.RemoveListener(value);
    }

    private float _health;
    private float _maxHealth;

    public float Health => _health;
    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        _health = _startHealth;
        _maxHealth = _health;
    }

    public void TakeDamage(float damage)
    {
        if (_health - damage < 0)
            _health = 0;
        else
            _health -= damage;

        _onHealthChange.Invoke();
    }

    public void Heal(float amount)
    {
        if (_health + amount > _maxHealth)
            _health = _maxHealth;
        else
            _health += amount;

        _onHealthChange.Invoke();
    }
}
