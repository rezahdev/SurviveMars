using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField]
    private GameObject alienPrefab;

    [SerializeField]
    private Transform[] alienSpawnPoints;

    [SerializeField]
    private int enemyCount = 16;

    private int enemyLimit;

    public float waitBeforeSpawnTime = 10f;


    void Awake()
    {
        InitEnemyManager(); 
    }
    void Start()
    {
        enemyLimit = enemyCount;
        SpawnEnemies();
        StartCoroutine("CheckToSpawnEnemies");
    }
    void InitEnemyManager()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void SpawnEnemies()
    {
        int index = 0;

        for(int i=0; i<enemyCount; i++)
        {
            if(index >= alienSpawnPoints.Length)
            {
                index = 0;
            }
            for(int j=0; j<2; j++)
            {
                Instantiate(alienPrefab, alienSpawnPoints[index].position, Quaternion.identity);
                alienPrefab.SetActive(true);
                alienPrefab.GetComponent<Animator>().enabled = true;
                alienPrefab.GetComponent<EnemyController>().enabled = true;
                alienPrefab.GetComponent<BoxCollider>().isTrigger = false;
                alienPrefab.GetComponent<NavMeshAgent>().enabled = true;
                alienPrefab.GetComponent<EnemyAnimator>().enabled = true;
            }
            index++;
        }
        enemyCount = 0;
    }
    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(waitBeforeSpawnTime);
        SpawnEnemies();
        StartCoroutine("CheckToSpawnEnemies");
    }
    public void EnemyHasDied()
    {
        enemyCount++;
        if (enemyCount > enemyLimit)
        {
            enemyCount = enemyLimit;
        }
    }
    public void StopSpawninning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }
}
