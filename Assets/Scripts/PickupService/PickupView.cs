using System.Collections;
using UnityEngine;

public class PickupView: MonoBehaviour
{
    private PickupController pickupController;
    [SerializeField] Animator pickUpAnimator;
    public void SetController(PickupController pickupController)
    {
        this.pickupController = pickupController;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==3)
        {
            pickupController.OnPickupEquipped();
        }
    }

    public Animator GetAnimator() => pickUpAnimator;

    public IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(pickupController.DestructionTimer);
        pickupController.ReturnPickup();
    }

    public void StartCoroutine()
    {
        pickupController.SetDestructionTimer(StartCoroutine(DestroyTimer()));
    }
    public void StopCoroutine()
    {
        StopCoroutine(DestroyTimer());
    }
}