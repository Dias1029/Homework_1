using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private float _health;
    [SerializeField] private float _fillSpeed;
    private float _healthPercents;

    private void Start()
    {
        _healthPercents = _health / 100;
    }

    public void MakeDamage(float damage)
    {
        if (_health > 0)
        {
            _health -= damage;
        }
        else
        {
            _health = 0;
        }

        _healthPercents = _health / 100;
    }

    private void Update()
    {
        _healthBar.value = Mathf.Lerp(_healthBar.value, _healthPercents, Time.deltaTime * _fillSpeed);
    }
}
