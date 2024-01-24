using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private string _tag="";
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _explosionParticle;

    private float _timerDestroy = 5f;

    private void Start()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }

    private void Update()
    {
        //_rigidbody.AddForce(transform.forward * _speed);
        //transform.Translate(Vector2.down*_speed*Time.deltaTime);


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
        _explosionParticle.Play();
        if(_explosionParticle.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
