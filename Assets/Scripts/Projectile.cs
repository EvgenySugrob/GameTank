using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _damage = 5;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private string _tag="";
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private string _poolerTag;

    private ObjectPooler _pooler;

    private float _timerDestroy = 5f;

    private void Start()
    {
        _pooler = ObjectPooler.instance;
        _rigidbody.velocity = transform.forward * _speed;
    }

    private void Update()
    {
        if(_timerDestroy <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            _timerDestroy -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Tank>() != null && other.gameObject.tag != _tag)
        {
            other.gameObject.GetComponent<Tank>().TakeDamage(_damage);
        }
        _rigidbody.velocity = Vector3.zero;
        _pooler.SpawnFromPool(_poolerTag, transform.position);
        Destroy(gameObject);
    }
}
