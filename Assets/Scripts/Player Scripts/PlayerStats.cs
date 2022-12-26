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
    private float scorePerKill = 3f;

    void Update()
    {
        UpdateScore();
    }
    public void DisplayHealthStat(float val)
    {
        healthStat.fillAmount = val / 100f;
    }
    public void DisplayStaminaStat(float val)
    {
        staminaStat.fillAmount = val / 100f;
    }
    public void UpdateScore(bool isEnemyKilled = false)
    {
        if(isEnemyKilled)
        {
            killCount++;
            scorePerKill += 0.5f; 
        }
        GlobalSettings.PlayerScore = Mathf.FloorToInt((killCount * scorePerKill) + 0.1f);
        enemyKillCountText.text = "Score: " + GlobalSettings.PlayerScore.ToString();
    }
    public void SetScoreToZero()
    {
        killCount = 0;
        enemyKillCountText.text = "Score: " + GlobalSettings.PlayerScore.ToString();
    }
}
