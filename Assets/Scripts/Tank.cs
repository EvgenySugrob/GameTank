using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Tank : MonoBehaviour
{
    [SerializeField] protected Transform _tower;
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _angleOffset;
    [SerializeField] protected float _rotationSpeed;
    [SerializeField] protected float _rotationTowerSpeed;
    [SerializeField] private int _points;
    protected Rigidbody _rigidbody;
    protected int _currentHealth;
   // protected UI _uiPlayer;

    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
        _rigidbody = GetComponent<Rigidbody>();
       // _uiPlayer = FindObjectOfType<UI>();
    }
    public virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth<=0)
        {
            Destroy(gameObject);
            //_uiPlayer.UpdateScoreAndLevel();
            //Stats.Score += _points;
        }
    }

    protected abstract void Move();

    protected abstract void RotationTower();

    protected void SetAngle(Vector3 target)
    {
        //Vector3 targetRotation = (target - transform.position);
        //targetRotation.y = 0;
        //Quaternion rotation = Quaternion.LookRotation(targetRotation);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime);

        //Vector3 deltaPosition = target - transform.position;
        //float angleY = Mathf.Atan2(deltaPosition.x, deltaPosition.z) * Mathf.Rad2Deg;
        //Quaternion angle = Quaternion.Euler(0, angleY + _angleOffset, 0);
        //transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * _rotationSpeed);
    }
}
