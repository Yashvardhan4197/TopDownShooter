using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyService : MonoBehaviour
{
    [SerializeField] List<EnemySpwanPointData> enemySpwanPointDatas=new List<EnemySpwanPointData>();
    [SerializeField] BoxCollider2D spawnTrigger;
    [SerializeField] int spawnTime;
    [SerializeField] EnemyView enemyPrefab;
    [SerializeField] Transform enemyContainerParent;
    [SerializeField] EnemyDataSO enemyDataSO;
    private EnemyPool enemyPool;
    private bool isSpawning;
    private float timer;
    public List<EnemySpwanPointData> EnemySpwanPointDatas {  get { return enemySpwanPointDatas; } }


    private async void Awake()
    {
        await Task.Delay(2*1000);
        enemyPool = new EnemyPool(enemyPrefab, enemyDataSO, enemyContainerParent,GameService.Instance.PlayerService.GetPlayerController().GetPlayerTransform());
    }

    public void OnGameStart()
    {
        isSpawning = false;
        spawnTrigger.isTrigger = true;
        timer = 0f;
    }

    public void SetSpawnCount(int spawnCount)
    {
        for (int i = 0;i<enemySpwanPointDatas.Count;i++) 
        {
            enemySpwanPointDatas[i].SpawnCount = spawnCount;
        }
    }

    private void Update()
    {
        if(isSpawning==true)
        {
            if(enemySpwanPointDatas.Count>0)
            {
                timer += Time.deltaTime;
                if (timer >= spawnTime)
                {
                    SpawnEnemyPrefab();
                    timer = 0f;
                }
            }
        }
    }

    private void SpawnEnemyPrefab()
    {
        //Instantiate enemy prefabs from each position
        for (int i = 0; i < enemySpwanPointDatas.Count; i++)
        {
            if (enemySpwanPointDatas[i].SpawnCount > 0)
            {
                EnemyController newEnemy = enemyPool.GetPooledItem();
                newEnemy.SetSpawnPosition(enemySpwanPointDatas[i].SpawnPosition.position);
                newEnemy.ActivateView();
                enemySpwanPointDatas[i].SpawnCount--;
                Debug.Log("Enemy Spawned");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isSpawning = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spawnTrigger.isTrigger = false;
    }

    [Serializable]
    public class EnemySpwanPointData
    {
        public int SpawnCount;
        public Transform SpawnPosition;
    }


}
