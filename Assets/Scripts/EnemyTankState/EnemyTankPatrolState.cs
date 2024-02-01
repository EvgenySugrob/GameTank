using SensorToolkit;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankPatrolState : EnemyTankBaseState
{
    private Vector3 _minPosition = new Vector3(-45f,0f,-45f);
    private Vector3 _maxPosition = new Vector3(45f, 0f, 45f);
    private Vector3 _patrolNextPoint;
    private NavMeshAgent _agent;

    public override void EnterState(EnemyTankStateManager enemyTank)
    {
        Debug.Log("Patrol!");
        _agent = enemyTank.gameObject.GetComponent<NavMeshAgent>();
        _agent.isStopped = false;
        _patrolNextPoint = new Vector3(Random.Range(_minPosition.x, _maxPosition.x), 0f, Random.Range(_minPosition.z, _maxPosition.z));
        _agent.SetDestination(_patrolNextPoint);
    }

    public override void UpdateState(EnemyTankStateManager enemyTank)
    {
        if (Vector3.Distance(enemyTank.transform.position, _patrolNextPoint) <= 5f)
        {
            enemyTank.SwitchState(enemyTank.checkState);
            //_patrolNextPoint = new Vector3(Random.Range(_minPosition.x, _maxPosition.x), 0f, Random.Range(_minPosition.z, _maxPosition.z));
            //_agent.SetDestination(_patrolNextPoint);
        }
    }

    public override void ExitState(EnemyTankStateManager enemyTank)
    {
        throw new System.NotImplementedException();
    }

    public override void GetTargetTowerSensor(Transform target, Transform tower, TriggerSensor sensor, float distant, float towerSpeedRotation)
    {
        
    }

    public override void GetTargetTowerSensor(Transform tower)
    {
        throw new System.NotImplementedException();
    }
}
