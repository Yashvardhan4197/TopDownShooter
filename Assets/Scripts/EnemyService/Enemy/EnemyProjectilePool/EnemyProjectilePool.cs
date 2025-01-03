﻿
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectilePool
{
    private List<PooledItem> pooledItems = new List<PooledItem>();
    private EnemyProjectileView enemyProjectilePrefab;

    private EnemyProjectileView CreatePooledItem()
    {
        PooledItem item = new PooledItem();
        item.enemyProjectileView = Object.Instantiate(enemyProjectilePrefab);
        item.isUsed = true;
        pooledItems.Add(item);
        return item.enemyProjectileView;
    }

    public EnemyProjectilePool(EnemyProjectileView enemyProjectilePrefab)
    {
        this.enemyProjectilePrefab = enemyProjectilePrefab;
        GameService.Instance.StartGameAction += OnGameStart;
    }

    public EnemyProjectileView GetPooledItem()
    {
        PooledItem item = pooledItems.Find(item => item.isUsed == false);
        if (item != null)
        {
            item.isUsed = true;
            return item.enemyProjectileView;
        }
        return CreatePooledItem();
    }

    public void ReturnToPool(EnemyProjectileView enemyProjectileView)
    {
        PooledItem item = pooledItems.Find(item => item.enemyProjectileView == enemyProjectileView);
        if (item != null)
        {
            item.isUsed = false;
        }
    }

    public void OnGameStart()
    {
        foreach(var item in pooledItems)
        {
            item.enemyProjectileView.gameObject.SetActive(false);
            ReturnToPool(item.enemyProjectileView);
        }
    }

    public class PooledItem
    {
        public EnemyProjectileView enemyProjectileView;
        public bool isUsed;
    }
}

