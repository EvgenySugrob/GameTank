using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTank : Tank
{
    [SerializeField] int _damage = 5;
    Transform _target;
    float _timer;
    [SerializeField] float _hitCoolddown = 1f;

    protected override void Start()
    {
        base.Start();
        _target = FindObjectOfType<Player>().transform;
    }
    protected override void Move()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null && _timer<=0)
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(_damage);
            _timer = _hitCoolddown;
        }
    }

    private void Update()
    {
        if(_target != null)
        {
            if (_timer <= 0)
            {
                Move();
                SetAngle(_target.position);
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
    }

    protected override void RotationTower()
    {
        
    }
}
