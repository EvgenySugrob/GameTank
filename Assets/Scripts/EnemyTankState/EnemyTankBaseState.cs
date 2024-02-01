using SensorToolkit;
using UnityEngine;

public abstract class EnemyTankBaseState
{
    public abstract void EnterState(EnemyTankStateManager enemyTank);
    public abstract void UpdateState(EnemyTankStateManager enemyTank);
    public abstract void ExitState(EnemyTankStateManager enemyTank);
    public abstract void GetTargetTowerSensor(Transform tower);
    public abstract void GetTargetTowerSensor(Transform target, Transform tower, TriggerSensor sensor,float distant,float towerSpeedRotation);
}
