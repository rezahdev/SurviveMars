using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim {
    NONE, 
    SELF_AIM,
    AIM
}

public enum WeaponFireType {
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType {
    BULLET,
    NONE
}

public class WeaponHandler : MonoBehaviour
{
    private Animator animator;
  
    [SerializeField]
    private GameObject muzzelFlush;

    [SerializeField]
    private AudioSource shootSound, reloadSound;

    private bool isTriggeredByWeaponChange;

    public WeaponAim weaponAim;
    public WeaponBulletType bulletType;
    public WeaponFireType fireType;
    public GameObject attackPoint;

    void Awake()
    {
        isTriggeredByWeaponChange = false;
        animator = GetComponent<Animator>();
    }
    public void Shoot()
    {
        animator.SetTrigger(AnimationTag.SHOOT_TRIGGER);
    }
    public void Aim(bool canAim) 
    {
        animator.SetBool(AnimationTag.AIM_PARAMETER, canAim);
    }
    void TurnOnMuzzleFlush()
    {
        if(!isTriggeredByWeaponChange)
        {
            muzzelFlush.SetActive(true);
        }
        else
        {
            isTriggeredByWeaponChange = false;
        }
    }
    void TurnOffMuzzleFlush()
    {
        muzzelFlush.SetActive(false);
    }
    void PlayShootSound()
    {
        if(!isTriggeredByWeaponChange)
        {
            shootSound.Play();
        }
        else
        {
            isTriggeredByWeaponChange = false;
        }    
    }
    void PlayReloadSound()
    {
        reloadSound.Play();
    }
    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }
    void TurnOffAttackPoint()
    {
        if(attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }
}
