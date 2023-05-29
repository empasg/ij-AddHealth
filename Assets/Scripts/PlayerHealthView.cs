using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Slider _healthSlider;
    [SerializeField][Min(0.001f)] private float _healthChangeTime;

    private float _lastHealth;
    private float _sliderLerpTime;
    private Coroutine _healthChangeCoroutine;

    private void Start()
    {
        Render();

        _playerHealth.OnHealthChange += Render;
    }

    public void Render()
    {
        if (_healthChangeCoroutine != null)
            StopCoroutine(_healthChangeCoroutine);

        _healthChangeCoroutine = StartCoroutine(ChangeSliderValue());
    }

    private IEnumerator ChangeSliderValue()
    {
        while(_sliderLerpTime / _healthChangeTime < 1 )
        {
            _healthSlider.value = Mathf.Lerp(_lastHealth, _playerHealth.Health, _sliderLerpTime / _healthChangeTime) / _playerHealth.MaxHealth;
            _sliderLerpTime += Time.deltaTime;

            yield return null;
        }

        _sliderLerpTime = 0;
        _lastHealth = _playerHealth.Health;
    }
}
