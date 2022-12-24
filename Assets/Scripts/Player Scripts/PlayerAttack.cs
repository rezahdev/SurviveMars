using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;

    public float fireRate = 15f;
    public float damage = 20f;
    private float nextTimeToFire;

    private Animator zoomAnimator;
    private bool isZoomed;

    private Camera mainCamera;
    private GameObject crosshair;

    void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        zoomAnimator = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        ZoomInAndOut();
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

    void ZoomInAndOut()
    {
        
        if(weaponManager.GetCurrentWeapon().weaponAim == WeaponAim.AIM)
        {
            if(Input.GetMouseButtonDown(1))
            {
                zoomAnimator.Play(AnimationTag.ZOOM_IN);
                crosshair.SetActive(false);
            }
            if(Input.GetMouseButtonUp(1))
            {
                zoomAnimator.Play(AnimationTag.ZOOM_OUT);
                crosshair.SetActive(true);
            }
        }
    }
}
