using SensorToolkit;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankAttackState : EnemyTankBaseState
{
    private Transform _target;
    private Transform _tower;
    private TriggerSensor _triggerSensor;
    private NavMeshAgent _agent;
    private List<GameObject> _targetsList;
    private float _distantToPlayer;
    private float _rotationTowerSpeed;
    private float _timer;
    private bool _isMove;

    public override void EnterState(EnemyTankStateManager enemyTank)
    {
        Debug.Log("Attack!");
        _agent = enemyTank.GetComponent<NavMeshAgent>();
        
    }

    public override void UpdateState(EnemyTankStateManager enemyTank)
    {
        _targetsList = _triggerSensor.GetDetected();
        if (_targetsList .Contains(_target.gameObject))
        {
            RotationTower();
            if (Vector3.Distance(enemyTank.transform.position, _target.position) >= _distantToPlayer)
            {
                if (_isMove == false)
                {
                    _agent.SetDestination(_target.position);
                    _isMove = true;
                }
            }

            if(_timer<=0 && Vector3.Distance(enemyTank.transform.position,_target.position)<= _distantToPlayer)
            {
                _isMove= false;
                enemyTank.GetComponent<RangeTank>().Shoot();
                _timer = enemyTank.GetComponent<RangeTank>()._reloadTime;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
        else
        {
            _agent.isStopped = true;
            _targetsList= null;
            enemyTank.GetComponent<RangeTank>()._isAttack= false;
            enemyTank.SwitchState(enemyTank.checkState);
        }
    }

    public override void ExitState(EnemyTankStateManager enemyTank)
    {
        
    }

    public override void GetTargetTowerSensor(Transform target, Transform tower, TriggerSensor sensor, float distant, float towerSpeedRotation)
    {
        _target = target;
        _tower = tower;
        _triggerSensor = sensor;
        _distantToPlayer = distant;
        _rotationTowerSpeed= towerSpeedRotation;
    }

    public override void GetTargetTowerSensor(Transform tower)
    {
        throw new System.NotImplementedException();
    }

    private void RotationTower()
    {
        Vector3 target = (_target.position - _tower.position);
        target.y = 0;
        Quaternion rotation = Quaternion.LookRotation(target);
        _tower.rotation = Quaternion.Lerp(_tower.rotation, rotation, _rotationTowerSpeed * Time.fixedDeltaTime);
    }
}
