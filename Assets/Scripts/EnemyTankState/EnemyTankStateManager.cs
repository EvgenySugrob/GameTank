using SensorToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankStateManager : MonoBehaviour
{
    private EnemyTankBaseState _currentState;
    public EnemyTankPatrolState patrolState = new EnemyTankPatrolState();
    public EnemyTankCheckState checkState = new EnemyTankCheckState();
    public EnemyTankAttackState attackState = new EnemyTankAttackState();

    private Transform _target;

    private void Start()
    {
        _currentState = patrolState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(EnemyTankBaseState state)
    {
        _currentState = state;
        state.EnterState(this);
    }

    public void GetTowerForCheck(Transform tower, Transform target, TriggerSensor sensor,float distantToPlayer,float towerRotationSpeed)
    {
        _target = target;
        checkState.GetTargetTowerSensor(tower);
        attackState.GetTargetTowerSensor(target,tower,sensor,distantToPlayer,towerRotationSpeed);
    }
}
