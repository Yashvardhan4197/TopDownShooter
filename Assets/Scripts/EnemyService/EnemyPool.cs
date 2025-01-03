﻿
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    private EnemyView enemyPrefab;
    private EnemyDataSO enemyDataSO;
    private Transform enemyContainerParent;
    private Transform playerTransform;
    private List<PooledItem> pooledItems=new List<PooledItem>();
    public EnemyPool(EnemyView enemyPrefab, EnemyDataSO enemyDataSO, Transform enemyContainerParent, Transform playerTransform)
    {
        this.enemyPrefab = enemyPrefab;
        this.enemyDataSO = enemyDataSO;
        this.enemyContainerParent = enemyContainerParent;
        this.playerTransform = playerTransform;
        GameService.Instance.StartGameAction += OnGameStart;
    }

    private EnemyController CreatePooledItem()
    {
        PooledItem item = new PooledItem();
        item.enemyController = new EnemyController(enemyPrefab, enemyDataSO, playerTransform, enemyContainerParent, this);
        item.isUsed = true;
        pooledItems.Add(item);
        return item.enemyController;
    }

    public EnemyController GetPooledItem()
    {
        PooledItem item = pooledItems.Find(item => item.isUsed == false);
        if(item != null)
        {
            item.isUsed = true;
            return item.enemyController;
        }
        return CreatePooledItem();
    }

    public void ReturnToPool(EnemyController enemyController)
    {
        PooledItem item=pooledItems.Find(item=>item.enemyController==enemyController);
        if(item != null)
        {
            item.isUsed=false;
        }
    }

    public void OnGameStart()
    {
        foreach(var item in pooledItems)
        {
            item.enemyController.OnEnemyDestroyed();
        }
    }


    public class PooledItem
    {
        public EnemyController enemyController;
        public bool isUsed;
    }
}