using System;
using System.Collections.Generic;

public class PickupPool
{
    private List<PooledItem> pooledItems = new List<PooledItem>();
    private PickupView pickupPrefab;
    private PickupDataSO pickupDataSO;

    public PickupPool(PickupView pickupPrefab, PickupDataSO pickupDataSO)
    {
        this.pickupDataSO = pickupDataSO;
        this.pickupPrefab = pickupPrefab;
        GameService.Instance.StartGameAction += OnGameStart;
    }

    public PickupController GetPooledItem()
    {
        PooledItem item = pooledItems.Find(item => item.isUsed == false);
        if(item != null)
        {
            item.isUsed = true;
            return item.pickupController;
        }
        return CreatePooledItem();
    }

    private PickupController CreatePooledItem()
    {
        PooledItem item = new PooledItem();
        item.pickupController=new PickupController(this.pickupPrefab,pickupDataSO);
        item.isUsed = true;
        pooledItems.Add(item);
        return item.pickupController;
    }

    public void ReturnToPool(PickupController pickupController)
    {
        PooledItem item = pooledItems.Find(item=>item.pickupController == pickupController);
        if(item != null )
        {
            item.isUsed = false;
        }
    }

    public void OnGameStart()
    {
        foreach(var item in pooledItems)
        {
            item.pickupController.ReturnPickup();
        }
    }
    public class PooledItem
    {
        public PickupController pickupController;
        public bool isUsed;
    }
}