using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navAgent;
    private EnemyController enemyController;
    private EnemyAudio enemyAudio;
    private PlayerStats playerStats;

    private bool isDead;

    public float health = 100f;
    public bool isPlayer, isAlien;

    private WeaponManager weaponManager;

    void Awake()
    {
        if (isAlien)
        {
            enemyAnimator = GetComponent<EnemyAnimator>();
            navAgent = GetComponent<NavMeshAgent>();
            enemyController = GetComponent<EnemyController>();
            enemyAudio = GetComponentInChildren<EnemyAudio>();
            playerStats = GameObject.FindGameObjectsWithTag(Tags.PLAYER_TAG)[0].GetComponent<PlayerStats>();
        }
        if (isPlayer)
        {
            playerStats = GetComponent<PlayerStats>();
        }
    }
    public void ApplyDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        if(isPlayer)
        {
            playerStats.DisplayHealthStat(health);
        }

        if(isAlien && enemyController.EnemyState == EnemyState.PATROL)
        {
            enemyController.chaseDistance = 50f;
        }

        if(health <= 0f)
        {
            PlayerHasDied();
            isDead = true;
        }
    }
    void PlayerHasDied()
    {
        if(isAlien)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 10f);

            enemyController.enabled = false;
            navAgent.enabled = false;
            enemyAnimator.enabled = false;

            StartCoroutine(EnemyDeathSound());
            EnemyManager.instance.EnemyHasDied();

            playerStats.UpdateScore(true);
        }

        if(isPlayer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyController>().enabled = false;
            }

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentWeapon().gameObject.SetActive(false);

            EnemyManager.instance.StopSpawninning();
 
        }

        if(tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }
    }
    void RestartGame()
    {
        GameObject.Find("Gameplay Controller").GetComponent<GameplayController>().EndGame();
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
    IEnumerator EnemyDeathSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.PlayDeathSound();
    }
}
