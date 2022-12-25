using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navAgent;
    private EnemyController enemyController;

    public float health = 100f;

    public bool isPlayer, isAlien;

    private bool isDead;

    private EnemyAudio enemyAudio;

    private PlayerStats playerStats;
    void Awake()
    {
        if(isAlien)
        {
            enemyAnimator = GetComponent<EnemyAnimator>();
            navAgent = GetComponent<NavMeshAgent>();
            enemyController = GetComponent<EnemyController>();  

            enemyAudio = GetComponentInChildren<EnemyAudio>();
        }
        if(isPlayer)
        {
            playerStats = GetComponent<PlayerStats>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float damage)
    {
        if(isDead)
        {
            return;
        }

        health -= damage;

        if(isPlayer)
        {
            playerStats.DisplayHealthStat(health);
        }

        if(isAlien)
        {
            if(enemyController.EnemyState == EnemyState.PATROL)
            {
                enemyController.chaseDistance = 50f;
            }
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
            //enemy manager - spawn more enemnies
        }

        if(isPlayer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyController>().enabled = false;
            }

            // call enemey manager to stop spawan enemies
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentWeapon().gameObject.SetActive(false);

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
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
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
