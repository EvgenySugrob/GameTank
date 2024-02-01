using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemy : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Slider _mainHealt;
    [SerializeField] private Slider _easyHealth;
    [SerializeField] private float _speedUpdate = 4f;

    private Transform _targetRotation;

    internal void SetStartParam(float maxHealt, Transform targetRotation)
    {
        _mainHealt.maxValue = maxHealt;
        _easyHealth.maxValue = maxHealt;
        _mainHealt.value = maxHealt;
        _easyHealth.value = maxHealt;
        _targetRotation= targetRotation;
    }

    public void UpdateHealthBar(float currentHealth)
    {
        _mainHealt.value = currentHealth;
    }

    private void Update()
    {
        transform.LookAt(_targetRotation);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        if (_easyHealth.value != _mainHealt.value)
        {
            _easyHealth.value = Mathf.Lerp(_easyHealth.value, _mainHealt.value, _speedUpdate * Time.deltaTime);
        }
    }
}
