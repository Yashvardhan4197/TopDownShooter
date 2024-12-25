
using UnityEngine;

public class IndividualPickupSpawner : MonoBehaviour
{
    [SerializeField] PickupType pickupType;


    private void Start()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        GameService.Instance.PickupService.SpawnIndividualPickUp(this.transform.position, pickupType);
    }


}
