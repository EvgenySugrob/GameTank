using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : ShootableTank
{
    private float _timer;
    private Camera _mainCamera;
    [Header("Tower Rotation")]
    [SerializeField] Transform _pivotTower;
    [SerializeField] Transform _pivotTurret;
    [SerializeField] Transform _cameraRig;
    [SerializeField] Transform _mouseAim;
    [SerializeField] float _minAngleTurret = -15f;
    [SerializeField] float _maxAngleTurret = 15f;
    [SerializeField] private UIPlayer _uiPlayer;

    protected override void Start()
    {
        base.Start();
        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible = false;
        _mainCamera = Camera.main;
        _uiPlayer = FindObjectOfType<UIPlayer>();
        _uiPlayer.SetStartParam(_maxHealth);
    }
    public override void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _uiPlayer.UpdateHealthBar(_currentHealth);
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
        //Поворот только башни
        Vector3 target = _mainCamera.transform.position - _tower.position;
        target = target * (-1);
        target.y = 0;
        Quaternion rotation = Quaternion.LookRotation(target);
        _tower.rotation = Quaternion.Lerp(_tower.rotation, rotation, _rotationTowerSpeed * Time.deltaTime);

        ////Подъем дула
        Vector3 tower = _cameraRig.rotation.eulerAngles;
        tower.x = (tower.x > 180) ? tower.x - 360 : tower.x;
        tower.x = Mathf.Clamp(tower.x, _minAngleTurret, _maxAngleTurret);
        _pivotTurret.localRotation = Quaternion.Euler(tower.x, 0, 0);

        //Поворот пивота башни
        Vector3 pivotTower = _tower.rotation.eulerAngles;
        Quaternion pivotRotation = Quaternion.Euler(tower.x, pivotTower.y, 0);
        _pivotTower.rotation = Quaternion.Lerp(_pivotTower.rotation, pivotRotation, _rotationTowerSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
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
