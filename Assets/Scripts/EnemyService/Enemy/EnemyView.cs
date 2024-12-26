
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyView : MonoBehaviour,IDamageAble
{
    private EnemyController enemyController;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] CircleCollider2D circleCollider;
    public void SetController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    public async void TakeDamage(int damage)
    {
        enemyController?.TakeDamage(damage);
        if (this != null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            await Task.Delay(1 * 100);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void Update()
    {
        enemyController?.MoveTowardsPlayer();
        enemyController?.AttackPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Attack Animation
        if(collision.gameObject.layer==3)
        {
            enemyController.StartAttacking();
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            enemyController.StopAttacking();
        }
    }

    public Animator GetAnimator() => enemyAnimator;
    public void SetAnimatorController(AnimatorController animator)
    {
        enemyAnimator.runtimeAnimatorController = animator;
    }

    public void OnDeathAnimationStart()
    {
        float deathAnimationDuration = enemyAnimator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("OnEnemyDeathAnimationComplete", deathAnimationDuration+.2f);
    }

    private void OnEnemyDeathAnimationComplete()
    {
        enemyController?.OnEnemyDestroyed();
        enemyController?.SpawnPickup();
    }

    public CircleCollider2D GetCircleCollider()=>circleCollider;

}
