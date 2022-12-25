using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;

    [SerializeField]
    private GameObject alienPrefab;

    [SerializeField]
    public Transform[] alienSpawnPoints;

    [SerializeField]
    private int enemyCount = 16;
    private int initialEnemyCount;

    public float waitBeforeSpawnTime = 10f;
    private int spawnMultiplier = 1;

    // Start is called before the first frame update
    void Awake()
    {
        InitEnemyManager();
    }

    void Start()
    {
        initialEnemyCount = enemyCount;

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static EnemyManager InitEnemyManager()
    {
        if(instance == null)
        {
            instance = new EnemyManager();
        }
        return instance;
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
        if (enemyCount > initialEnemyCount)
        {
            enemyCount = initialEnemyCount;
        }
    }

    public void StopSpawninning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }
}
