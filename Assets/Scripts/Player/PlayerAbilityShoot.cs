using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public Transform gunPosition;

    private GunBase _currentGun;
    private int _currentGunIndex = 0;
    public GunBase[] gunBases;

    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.canceled += cts => CancelShoot();
        inputs.Gameplay.ChangeGun.performed += cts => SwapWeapon();
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(gunBases[_currentGunIndex], gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        Debug.Log("Start Shoot");
    }

    private void CancelShoot()
    {
        _currentGun.StopShoot();
        Debug.Log("Stop Shoot");
    }

    private void SwapWeapon()
    {
        _currentGunIndex = (_currentGunIndex + 1) % gunBases.Length;
        Destroy(_currentGun.gameObject);
        CreateGun();
    }
}
