using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoBehaviour
{
    [SerializeField] List<EnemySpwanPointData> enemySpwanPointDatas=new List<EnemySpwanPointData>();
    [SerializeField] BoxCollider2D spawnTrigger;
    [SerializeField] int spawnTime;
    private bool isSpawning;
    private float timer;
    public List<EnemySpwanPointData> EnemySpwanPointDatas {  get { return enemySpwanPointDatas; } }

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
        Debug.Log("Enemy Spawned");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isSpawning = true;
        StartEnemySpawning();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spawnTrigger.isTrigger = false;
    }

    private void StartEnemySpawning()
    {
        
    }

    [Serializable]
    public class EnemySpwanPointData
    {
        public int SpawnCount;
        public Transform SpawnPosition;
    }


}
