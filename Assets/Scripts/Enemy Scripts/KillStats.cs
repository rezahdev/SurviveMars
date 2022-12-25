using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillStats : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI enemyKillCountText;

    public void UpdateKillCount(int enemyKillCount)
    {
        enemyKillCountText.text = "Alien Killed: " + enemyKillCount.ToString();
    }
}
