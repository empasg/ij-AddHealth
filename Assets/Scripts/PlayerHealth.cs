using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField][Min(1)] private int _maxHealth;
    [SerializeField] private UnityEvent _healthChange = new UnityEvent();

    public event UnityAction HealthChange
    {
        add => _healthChange.AddListener(value);
        remove => _healthChange.RemoveListener(value);
    }

    private float _health;

    public float Health => _health;
    public float MaxHealth => _maxHealth;

    private void Start()
    {
        Heal(_maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (_health - damage < 0)
            _health = 0;
        else
            _health -= damage;

        _healthChange.Invoke();
    }

    public void Heal(float amount)
    {
        if (_health + amount > _maxHealth)
            _health = _maxHealth;
        else
            _health += amount;

        _healthChange.Invoke();
    }
}
