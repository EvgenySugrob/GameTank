using SensorToolkit;
using System.Collections;
using UnityEngine;

public class EnemyTankCheckState : EnemyTankBaseState
{
    private Transform _tower;
    private Quaternion _defaultRotation;
    private float _timer;
    private bool _isCheck;
    private float _rotationTowerSpeed = 2f;


    public override void EnterState(EnemyTankStateManager enemyTank)
    {
        Debug.Log("Check!");
        
        _timer = Random.Range(4, 7);
        _isCheck = true;
    }

    public override void UpdateState(EnemyTankStateManager enemyTank)
    {
        if (_timer >= 0 && _isCheck)
        {
            _tower.localEulerAngles = new Vector3(0, Mathf.PingPong(Time.time * 30, 120) - 60, 0);

            _timer -= Time.deltaTime;
        }
        else
        {
            _isCheck = false;
        }

        if(_isCheck == false)
        {
            _tower.localRotation = Quaternion.Lerp(_tower.localRotation, _defaultRotation, Time.deltaTime * _rotationTowerSpeed);

            if (_tower.localRotation == _defaultRotation)
                enemyTank.SwitchState(enemyTank.patrolState);
        }
    }

    public override void ExitState(EnemyTankStateManager enemyTank)
    {
        
    }

    public override void GetTargetTowerSensor(Transform target, Transform tower, TriggerSensor sensor, float distant, float towerSpeedRotation)
    {
        throw new System.NotImplementedException();
    }

    public override void GetTargetTowerSensor(Transform tower)
    {
        _tower = tower;
        _defaultRotation = _tower.localRotation;
    }
}
