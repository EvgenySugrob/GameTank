using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : ShootableTank
{
    private float _timer;
    private Camera _mainCamera;

    protected override void Start()
    {
        base.Start();
        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible = false;
        _mainCamera = Camera.main;
    }
    public override void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        //_uiPlayer.UpdateHp(_currentHealth);
        if(_currentHealth<=0)
        {
            // Stats.ResetAllStats();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    protected override void Move()
    {

        Vector3 direction = transform.forward * Input.GetAxis("Vertical");
        _rigidbody.velocity = direction.normalized * _speed*Time.fixedDeltaTime;

        float rotation = Input.GetAxis("Horizontal") * _rotationSpeed * Time.fixedDeltaTime;
        Quaternion towerRotation = Quaternion.Euler(0, rotation, 0);
        _rigidbody.MoveRotation(_rigidbody.rotation * towerRotation);
    }

    protected override void RotationTower()
    {
        Vector3 target = (_mainCamera.transform.position - _tower.position);
        target = target * (-1);
        target.y = 0;
        Quaternion rotation = Quaternion.LookRotation(target);
        _tower.rotation = Quaternion.Lerp(_tower.rotation, rotation, _rotationTowerSpeed * Time.fixedDeltaTime);

    }
    private void FixedUpdate()
    {
        Move();
        RotationTower();
    }
    private void Update()
    {
       
        //SetAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (_timer<=0)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
                _timer = _reloadTime;
            }
        }
        else
        {
            _timer -= Time.deltaTime;
        }
        
    }
}
