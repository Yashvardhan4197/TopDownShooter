using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyProjectileView: MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] int lifeTime;
    private Vector2 direction;
    private float damage;
    //private Vector3 lastPlayerPos;


    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetTransformPosition(Vector3 position)
    {
        this.gameObject.SetActive(true);
        rb2D.velocity = Vector2.zero;
        this.transform.position= position;
        Transform playerTransform = GameService.Instance.PlayerService.GetPlayerController().GetPlayerTransform();
        if(playerTransform == null )
        {
            Debug.Log("tfff");
        }
        direction = (playerTransform.transform.position - transform.position).normalized;
        //this.lastPlayerPos = lastPlayerPos;
        // Rotate the projectile to face the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle-90);
        rb2D.velocity = direction * speed;
        Invoke(nameof(DestroyPorjectile), lifeTime);
    }

    private void Update()
    {
       // Vector2 currentPosition = transform.position;
        //Vector2 direction = (direction - currentPosition).normalized; // Calculate direction
       //transform.position=Vector3.MoveTowards(currentPosition,lastPlayerPos,speed* Time.deltaTime);
        //if (Vector2.Distance(currentPosition, lastPlayerPos) < 0.1f)
        //{
        //    GameService.Instance.EnemyProjectilePool.ReturnToPool(this);
        //    this.gameObject.SetActive(false);   
       // }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==3) 
        {
            Debug.Log("projectile hit");
            GameService.Instance.PlayerService.GetPlayerController().TakeDamage(damage);
            GameService.Instance.EnemyProjectilePool.ReturnToPool(this);
            this.gameObject.SetActive(false);
        }else if(collision.gameObject.layer==8)
        {
            DestroyPorjectile();
        }
    }

    private void DestroyPorjectile()
    {
        GameService.Instance.EnemyProjectilePool.ReturnToPool(this);
        CancelInvoke(nameof(DestroyPorjectile));
        this.gameObject.SetActive(false);
        
    }

}