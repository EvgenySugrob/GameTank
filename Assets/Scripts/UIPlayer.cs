using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider _mainHealt;
    [SerializeField] private Slider _easyHealth;
    [SerializeField] private float _speedUpdate = 1.5f; 

    internal void SetStartParam(float maxHealt)
    {
        _mainHealt.maxValue= maxHealt;
        _easyHealth.maxValue= maxHealt;
        _mainHealt.value= maxHealt;
        _easyHealth.value= maxHealt;
    }

    public void UpdateHealthBar(float currentHealth)
    {
        _mainHealt.value = currentHealth;
    }

    private void Update()
    {
        if(_easyHealth.value!=_mainHealt.value)
        {
            _easyHealth.value = Mathf.Lerp(_easyHealth.value, _mainHealt.value, _speedUpdate * Time.deltaTime);
        }
    }
}
