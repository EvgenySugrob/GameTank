using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootableTank : Tank
{
    [Header("Shoot param")]
    [SerializeField] string _projectileTag;
    [SerializeField] GameObject _projectile;
    [SerializeField] Transform _shotPoint;
    [SerializeField] public float _reloadTime = 0.5f;
    private ObjectPooler _objectPooler;

    protected override void Start()
    {
        base.Start();
        //_objectPooler = ObjectPooler.instance;
    }

    public void Shoot()
    {
        Instantiate(_projectile, _shotPoint.position, _shotPoint.rotation);
        //_objectPooler.SpawnFromPool(_projectileTag,_shotPoint.position,transform.rotation);
    }
}
