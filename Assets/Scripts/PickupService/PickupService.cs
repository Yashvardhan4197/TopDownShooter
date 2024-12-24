
using UnityEngine;

public class PickupService 
{
    private PickupPool pickupPool;
    private float pickupProbabilityRate;

    public PickupService(PickupDataSO pickupDataSO, PickupView pickupPrefab, float pickupProbabilityRate) 
    {
        pickupPool = new PickupPool(pickupPrefab, pickupDataSO);
        this.pickupProbabilityRate = pickupProbabilityRate;
    }

    public void SpawnPickUp(Vector3 spawnPosition)
    {
        int rand = Random.Range(0, 100);
        if(rand<=pickupProbabilityRate)
        {
            PickupController pickup = pickupPool.GetPooledItem();
            pickup.InitializePickup(spawnPosition);
        }

    }

    public void ReturnToPool(PickupController pickupController)
    {
        pickupPool.ReturnToPool(pickupController);
    }

}
