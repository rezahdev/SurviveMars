using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Image healthStat, staminaStat;

    [SerializeField]
    private TextMeshProUGUI enemyKillCountText;

    private int killCount = 0;

    public void DisplayHealthStat(float val)
    {
        healthStat.fillAmount = val / 100f;
    }
    public void DisplayStaminaStat(float val)
    {
        staminaStat.fillAmount = val / 100f;
    }
    public void UpdateKillCount()
    {
        print(killCount++);
        enemyKillCountText.text = "Kills: " + killCount.ToString();
    }
}
