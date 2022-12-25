using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField]
    private GameObject alienPrefab;

    [SerializeField]
    public Transform[] alienSpawnPoints;

    [SerializeField]
    private int enemyCount = 16;
    private int enemyLimit;

    public float waitBeforeSpawnTime = 10f;
    private int spawnMultiplier = 1;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
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
        print("Spawninng enemies ..." + enemyCount);
        int index = 0;

        for(int i=0; i<enemyCount; i++)
        {
            if(index >= alienSpawnPoints.Length)
            {
                index = 0;
            }
            for(int j=0; j<spawnMultiplier; j++)
            {
                Instantiate(alienPrefab, alienSpawnPoints[index].position, Quaternion.identity);
            }
            index++;
        }
        spawnMultiplier++;
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
        print(enemyCount + " / " + enemyLimit);
        if (enemyCount > enemyLimit)
        {
            enemyCount = enemyLimit;
        }
        print(enemyCount);
    }

    public void StopSpawninning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }
}
