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
        healthStat.fillAmount = val / 100f;
    }
    public void DisplayStaminaStat(float val)
    {
        staminaStat.fillAmount = val / 100f;
    }
    
}
