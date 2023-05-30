using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Slider _healthSlider;
    [SerializeField][Min(0.001f)] private float _healthChangeTime;

    private float _lastSliderValue;
    private float _sliderLerpTime;
    private Coroutine _changeSliderValue;

    private void OnEnable()
    {
        _playerHealth.HealthChange += OnHealthChange;
    }

    private void OnDisable()
    {
        _playerHealth.HealthChange -= OnHealthChange;
    }

    private void OnHealthChange()
    {
        if (_changeSliderValue != null)
            StopCoroutine(_changeSliderValue);

        _changeSliderValue = StartCoroutine(ChangeSliderValue());
    }

    private IEnumerator ChangeSliderValue()
    {
        _lastSliderValue = _healthSlider.value;
        _sliderLerpTime = 0;

        while ( ( _playerHealth.Health / _playerHealth.MaxHealth ).ToString("F4") != _healthSlider.value.ToString("F4") )
        {
            _healthSlider.value = Mathf.Lerp(_lastSliderValue, _playerHealth.Health / _playerHealth.MaxHealth, _sliderLerpTime / _healthChangeTime);
            _sliderLerpTime += Time.deltaTime;

            yield return null;
        }

        _changeSliderValue = null;
    }
}
