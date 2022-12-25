using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Image healthStat, staminaStat;
    
    public void DisplayHealthStat(float val)
    {
        val /= 100f;
        healthStat.fillAmount = val;
    }

    public void DisplayStaminaStat(float val)
    {
        val /= 100f;
        staminaStat.fillAmount = val;
    }
}
