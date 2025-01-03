﻿
using UnityEngine;

public class EnemyController
{
    private EnemyView enemyView;
    private Transform playerTransform;
    private EnemyDataSO enemyDataSO;
    private float health;
    private EnemyPool enemyPool;
    private bool isAttacking;
    private float nextTimeToAttack;
    private bool isMoving;
    private bool isDead;
    private Vector3 randomMovementOffset;
    private int currentSetEnemy;
    private EnemyService enemyService;

    public EnemyController(EnemyView enemyView,EnemyDataSO enemyDataSO,Transform playerTransform,Transform EnemyContainerParent,EnemyPool enemyPool)
    {
        this.enemyView=Object.Instantiate(enemyView);
        this.enemyView.transform.SetParent(EnemyContainerParent);
        this.playerTransform = playerTransform;
        this.enemyView.SetController(this);
        this.enemyDataSO=enemyDataSO;
        this.enemyPool= enemyPool;
        currentSetEnemy = -1;
    }

    private void RandomizeEnemy()
    {
        int rand = Random.Range(0, enemyDataSO.EnemyCollections.Count);
        currentSetEnemy = rand;
        enemyView.SetAnimatorController(enemyDataSO.EnemyCollections[currentSetEnemy].AnimatorController);
    }

    private void SetDirection(float angle)
    {
        if (angle > 180)
        {
            angle -= 360;
        }
        float x = Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = Mathf.Sin(angle * Mathf.Deg2Rad);

        int xDirection = 0;
        int yDirection = 0;

        if (Mathf.Abs(x) > 0.5f)
        {
            xDirection = x > 0 ? 1 : -1;
        }

        if (Mathf.Abs(y) > 0.5f)
        {
            yDirection = y > 0 ? 1 : -1;
        }
        enemyView.GetAnimator().SetFloat("X", xDirection);
        enemyView.GetAnimator().SetFloat("Y", yDirection);
    }

    public void ActivateView(EnemyService enemyService)
    {
        enemyView.gameObject.SetActive(true);
        isAttacking = false;
        isMoving = true;
        isDead = false;
        randomMovementOffset= new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        RandomizeEnemy();
        health = enemyDataSO.EnemyCollections[currentSetEnemy].Health;
        enemyView.GetAnimator().SetBool("isDead", isDead);
        enemyView.GetCircleCollider().radius = enemyDataSO.EnemyCollections[currentSetEnemy].AttackRadius;
        this.enemyService = enemyService;
        enemyView.GetSpriteRenderer().color=Color.white;
    }

    public void SetSpawnPosition(Vector3 spawnPosition)
    {
        enemyView.transform.position = spawnPosition;
    }

    public void MoveTowardsPlayer()
    {
        if(isMoving)
        {
            Vector3 targetPos = playerTransform.position + randomMovementOffset;
            Vector3 direction = (targetPos - enemyView.transform.position).normalized;
            enemyView.transform.position += direction * enemyDataSO.EnemyCollections[currentSetEnemy].MovementSpeed * Time.deltaTime;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            SetDirection(angle);
        }
        enemyView.GetAnimator().SetBool("isRunning", isMoving);
    }

    public void OnEnemyDestroyed()
    {
        enemyView.gameObject.SetActive(false);
        enemyPool.ReturnToPool(this);
        isAttacking=false;
        isDead=false;
    }

    public void AttackPlayer()
    {
        if(isAttacking==true&&isDead==false)
        {
            if (enemyDataSO.EnemyCollections[currentSetEnemy].EnemyType != EnemyType.BEE)
            {

                if (Time.time > nextTimeToAttack)
                {
                    nextTimeToAttack = Time.time + enemyDataSO.EnemyCollections[currentSetEnemy].AttackDelay;
                    GameService.Instance.PlayerService.GetPlayerController().TakeDamage(enemyDataSO.EnemyCollections[currentSetEnemy].Damage);
                }
            }else if(enemyDataSO.EnemyCollections[currentSetEnemy].EnemyType == EnemyType.BEE)
            {
                if (Time.time > nextTimeToAttack)
                {
                    nextTimeToAttack = Time.time + enemyDataSO.EnemyCollections[currentSetEnemy].AttackDelay;
                    EnemyProjectileView newEnemyProjectile = GameService.Instance.EnemyProjectilePool.GetPooledItem();
                    newEnemyProjectile.SetDamage(enemyDataSO.EnemyCollections[(currentSetEnemy)].Damage);
                    newEnemyProjectile.SetTransformPosition(enemyView.transform.position);
                }
            }
        }
    }

    public void StartAttacking()
    {
        nextTimeToAttack = 0f;
        isAttacking = true;
        isMoving = false;
        enemyView.GetAnimator().SetBool("isAttacking", isAttacking);
        
    }

    public void StopAttacking()
    {
        isAttacking = false;
        isMoving = true;
        enemyView.GetAnimator().SetBool("isAttacking", isAttacking);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0f)
        {
            if (isDead == false)
            {
                enemyView.GetAnimator().SetBool("isDead", true);
                enemyView.OnDeathAnimationStart();
                enemyService.ReduceSpawnedEnemyCount();
            }
            isDead = true; 
        }
    }

    public void SpawnPickup()
    {
        GameService.Instance.PickupService.SpawnPickUp(enemyView.transform.position);
    }
}