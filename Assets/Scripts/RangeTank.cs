using SensorToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeTank : ShootableTank
{
    [SerializeField] UIEnemy _uiEnemy;
    [SerializeField] float _distansToPlayer;
    [SerializeField] private TriggerSensor _triggerSensor;
    [HideInInspector] public float _timer;
    private Transform _target;
    private NavMeshAgent _agent;
    private EnemyTankStateManager _stateManager;
    public bool _isAttack;
    

    protected override void Start()
    {
        base.Start();
        _target = FindObjectOfType<Player>().transform;
        _uiEnemy.SetStartParam(_maxHealth, _target);
        _agent = GetComponent<NavMeshAgent>();
        _stateManager= GetComponent<EnemyTankStateManager>();
        
        _stateManager.GetTowerForCheck(_tower,_target,_triggerSensor,_distansToPlayer,_rotationTowerSpeed);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        _uiEnemy.UpdateHealthBar(_currentHealth);
    }

    protected override void Move()
    {
        //if (Time.time >= _pathUpdateDelay)
        //{
        //    _agent.SetDestination(_target.position);
        //}
    }

    private void Update()
    {
        List<GameObject> detectedList = _triggerSensor.GetDetected();
        if (detectedList.Contains(_target.gameObject) && _isAttack == false)
        {
            _stateManager.SwitchState(_stateManager.attackState);
            _isAttack = true;
        }
        //RotationTower();

        //if (Vector3.Distance(transform.position,_target.position)>=_distansToPlayer)
        //{
        //    Move();
        //    SetAngle(_target.position);
        //}

        //if (_timer<0 && Vector3.Distance(transform.position, _target.position) <= _distansToPlayer)
        //{
        //    Shoot();
        //    _timer = _reloadTime;
        //}
        //else
        //{
        //    _timer -= Time.deltaTime;
        //}
    }

    protected override void RotationTower()
    {
        Vector3 target = (_target.position - _tower.position);
        target.y = 0;
        Quaternion rotation = Quaternion.LookRotation(target);
        _tower.rotation = Quaternion.Lerp(_tower.rotation, rotation, _rotationTowerSpeed * Time.fixedDeltaTime);
    }
}