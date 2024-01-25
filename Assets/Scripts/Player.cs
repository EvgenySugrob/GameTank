using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : ShootableTank
{
    private float _timer;
    private Camera _mainCamera;
    [SerializeField] Transform _pivotTower;
    [SerializeField] Transform _pivotTurret;
    [SerializeField] Transform _cameraRig;

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
        //_cameraRig Привязка
        //_pivotTower.rotation = _cameraRig.rotation;

        
        //Поворот только башни
        Vector3 target = _mainCamera.transform.position - _tower.position;
        target = target * (-1);
        target.y = 0;
        Quaternion rotation = Quaternion.LookRotation(target);
        _tower.rotation = Quaternion.Lerp(_tower.rotation, rotation, _rotationTowerSpeed * Time.deltaTime);

        //поворот дула
        Vector3 tower = _cameraRig.rotation.eulerAngles;
        _tower.rotation = Quaternion.Euler(0, tower.y, 0);
        tower.x = (tower.x > 180) ? tower.x - 360 : tower.x;
        tower.x = Mathf.Clamp(tower.x, -15f, 15f);
        _pivotTurret.localRotation = Quaternion.Euler(tower.x, 0, 0);

        //Поворот пивота башни
        //Vector3 pivotTower = _tower.rotation.eulerAngles;
        //_pivotTower.rotation = Quaternion.Euler(tower.x, pivotTower.y, 0);
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {

        //SetAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        RotationTower();
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
