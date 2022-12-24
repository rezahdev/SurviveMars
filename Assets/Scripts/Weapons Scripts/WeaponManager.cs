using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;

    private int currentWeaponIndex;

    void Start()
    {
        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectWeapon(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectWeapon(3);
        }
        
    }

    void SelectWeapon(int weaponIndex) 
    {
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        weapons[weaponIndex].gameObject.SetActive(true);
        currentWeaponIndex = weaponIndex;
    }

    public WeaponHandler GetCurrentWeapon()
    {
        return weapons[currentWeaponIndex];
    }
}