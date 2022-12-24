using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;

    public float fireRate = 15f;
    public float damage = 20f;
    private float nextTimeToFire;

    void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        
        if(weaponManager.GetCurrentWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire) 
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                weaponManager.GetCurrentWeapon().Shoot();
                // BulletFired()
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(weaponManager.GetCurrentWeapon().tag == Tags.AXE_TAG)
                {
                    weaponManager.GetCurrentWeapon().Shoot();
                }

                if(weaponManager.GetCurrentWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    weaponManager.GetCurrentWeapon().Shoot();
                    // BulletFired()
                }
            }
        }
    }
}
