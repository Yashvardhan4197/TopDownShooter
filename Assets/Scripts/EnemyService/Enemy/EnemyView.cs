
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyView : MonoBehaviour,IDamageAble
{
    private EnemyController enemyController;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] CircleCollider2D circleCollider;
    [SerializeField] SpriteRenderer spriteRenderer;
    private bool coroutineStatus;

    private void Update()
    {
        enemyController?.MoveTowardsPlayer();
        enemyController?.AttackPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
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

    private void OnEnemyDeathAnimationComplete()
    {
        enemyController?.OnEnemyDestroyed();
        enemyController?.SpawnPickup();
    }

    private IEnumerator SetColor()
    {
        if (coroutineStatus == true)
        {
            spriteRenderer.color = Color.white;
            StopCoroutine(SetColor());
            coroutineStatus = false;
        }
        else
        {
            coroutineStatus = true;
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(1f);
            if (this != null)
            {
                spriteRenderer.color = Color.white;
            }
        }
    }

    public void SetController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    public void TakeDamage(int damage)
    {
        enemyController?.TakeDamage(damage);
        if (this != null)
        {
            StartCoroutine(SetColor());
        }
    }

    public Animator GetAnimator() => enemyAnimator;

    public void SetAnimatorController(RuntimeAnimatorController animator)
    {
        enemyAnimator.runtimeAnimatorController = animator;
    }

    public void OnDeathAnimationStart()
    {
        float deathAnimationDuration = enemyAnimator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("OnEnemyDeathAnimationComplete", deathAnimationDuration+.2f);
    }

    public CircleCollider2D GetCircleCollider()=>circleCollider;

    public SpriteRenderer GetSpriteRenderer() => spriteRenderer;

}
