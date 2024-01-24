using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTank : ShootableTank
{
    [SerializeField] float _distansToPlayer = 5f;
    private float _timer;
    private Transform _target;

    protected override void Start()
    {
        base.Start();
        _target = FindObjectOfType<Player>().transform;
    }

    protected override void Move()
    {
        //transform.Translate(Vector2.down * _speed * Time.deltaTime);
        Vector3 direction = transform.forward;
        _rigidbody.velocity = direction.normalized * _speed;

        Vector3 targetRotation = (_target.position - transform.position);
        targetRotation.y = 0;
        Quaternion rotation = Quaternion.LookRotation(targetRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        RotationTower();

        if (Vector3.Distance(transform.position,_target.position)>=_distansToPlayer)
        {
            Move();
            SetAngle(_target.position);
        }
        
        if (_timer<0 && Vector3.Distance(transform.position, _target.position) <= _distansToPlayer)
        {
            Shoot();
            _timer = _reloadTime;
        }
        else
        {
            _timer -= Time.fixedDeltaTime;
        }
    }

    protected override void RotationTower()
    {
        Vector3 target = (_target.position - _tower.position);
        target.y = 0;
        Quaternion rotation = Quaternion.LookRotation(target);
        _tower.rotation = Quaternion.Lerp(_tower.rotation, rotation, _rotationTowerSpeed * Time.fixedDeltaTime);
    }
}
