using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyService : MonoBehaviour
{
    [SerializeField] List<EnemySpwanPointData> enemySpwanPointDatas = new List<EnemySpwanPointData>();
    [SerializeField] BoxCollider2D spawnTrigger;
    [SerializeField] int spawnTime;
    //[SerializeField] EnemyView enemyPrefab;
    //[SerializeField] Transform enemyContainerParent;
    //[SerializeField] EnemyDataSO enemyDataSO;
    [SerializeField] GameObject[] DoorCollection;
    //private EnemyPool enemyPool;
    private bool isSpawning;
    private float timer;
    public List<EnemySpwanPointData> EnemySpwanPointDatas { get { return enemySpwanPointDatas; } }
    private int totalEnemies;

    private void Start()
    {
        OnGameStart();
    }


    private async void Awake()
    {
        //GameService.Instance.StartGameAction += OnGameStart;
        await Task.Delay(2 * 1000);
        //enemyPool = new EnemyPool(enemyPrefab, enemyDataSO, enemyContainerParent, GameService.Instance.PlayerService.GetPlayerController().GetPlayerTransform(), this);
    }

    public void OnGameStart()
    {
        isSpawning = false;
        spawnTrigger.isTrigger = true;
        timer = 0f;
        totalEnemies = 0;
        SetSpawnCount();
        OpenAllDoors();
    }

    public void SetSpawnCount()
    {
        for (int i = 0; i < enemySpwanPointDatas.Count; i++)
        {
            enemySpwanPointDatas[i].SetCurrentlySpawnCount(enemySpwanPointDatas[i].totalSpawnCount);
            totalEnemies += enemySpwanPointDatas[i].totalSpawnCount;
        }
    }

    private void Update()
    {
        if (isSpawning == true)
        {
            if (enemySpwanPointDatas.Count > 0)
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
            if (enemySpwanPointDatas[i].CurrentlySpawnCount > 0)
            {
                EnemyController newEnemy = GameService.Instance.EnemyPool.GetPooledItem();
                newEnemy.SetSpawnPosition(enemySpwanPointDatas[i].SpawnPosition.position);
                newEnemy.ActivateView(this);
                enemySpwanPointDatas[i].SetCurrentlySpawnCount(enemySpwanPointDatas[i].CurrentlySpawnCount - 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (totalEnemies > 0)
        {
            isSpawning = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (totalEnemies > 0 && isSpawning == true)
        {
            CloseAllDoors();
        }
        //spawnTrigger.isTrigger = false;
    }

    public void ReduceSpawnedEnemyCount()
    {
        totalEnemies--;
        if (totalEnemies <= 0)
        {
            isSpawning = false;
            OpenAllDoors();
        }
    }

    private void OpenAllDoors()
    {
        foreach (var item in DoorCollection)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void CloseAllDoors()
    {
        foreach (var item in DoorCollection)
        {
            item?.gameObject.SetActive(true);
        }
    }

    [Serializable]
    public class EnemySpwanPointData
    {
        private int currentlySpawnCount;
        public int totalSpawnCount;
        public Transform SpawnPosition;

        public void SetCurrentlySpawnCount(int count)
        {
            currentlySpawnCount = count;
        }

        public int CurrentlySpawnCount { get { return currentlySpawnCount; } }

    }


}
