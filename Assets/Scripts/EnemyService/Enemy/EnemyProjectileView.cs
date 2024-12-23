using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyProjectileView: MonoBehaviour
{
    [SerializeField] float speed;
    private Vector2 direction;
    private float damage;
    private Vector3 lastPlayerPos;
    private void Start()
    {

    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetTransformPosition(Vector3 position, Vector3 lastPlayerPos)
    {
        this.transform.position= position;
        Transform playerTransform = GameService.Instance.PlayerService.GetPlayerController().GetPlayerTransform();
        direction = (playerTransform.transform.position - transform.position).normalized;
        this.lastPlayerPos = lastPlayerPos;
        // Rotate the projectile to face the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle-90);
    }

    private void Update()
    {
        Vector2 currentPosition = transform.position;
        //Vector2 direction = (direction - currentPosition).normalized; // Calculate direction
        transform.position=Vector3.MoveTowards(currentPosition,lastPlayerPos,speed* Time.deltaTime);
        if (Vector2.Distance(currentPosition, lastPlayerPos) < 0.1f)
        {
            GameService.Instance.EnemyProjectilePool.ReturnToPool(this);
            this.gameObject.SetActive(false);   
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==3) 
        {
            Debug.Log("projectile hit");
            GameService.Instance.PlayerService.GetPlayerController().TakeDamage(damage);
            GameService.Instance.EnemyProjectilePool.ReturnToPool(this);
            this.gameObject.SetActive(false);
        }
    }

}