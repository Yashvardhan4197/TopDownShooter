
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyTurretManager : MonoBehaviour,IDamageAble
{
    [SerializeField] Transform gunTransform;
    [SerializeField] Transform muzzleTransform;
    [SerializeField] SpriteRenderer detectedSprite;
    [SerializeField] SpriteRenderer mainSprite;
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] float range;
    [SerializeField] float attackDelay;
    [SerializeField] float health;
    [SerializeField] float Attackdamage;
    private bool isDetected;
    private Transform playerTransform;
    private float nextTimeToFire;
    private float colorTime;
    private void Start()
    {
        isDetected = false;
        nextTimeToFire = 0f;
        playerTransform=GameService.Instance.PlayerService.GetPlayerController().GetPlayerTransform();
    }

    private void Update()
    {
        Vector2 direction = (playerTransform.position-muzzleTransform.position).normalized;
        RaycastHit2D hit2D=Physics2D.Raycast(muzzleTransform.position,direction,range,~ignoreLayer);

        if(hit2D!=false && hit2D.transform.gameObject.layer==3)
        {
            RotateTowardsPlayer(direction);
            if (isDetected==false)
            {
               
                isDetected = true;
                detectedSprite.color = Color.red;
            }
        }
        else
        {
            isDetected = false;
            detectedSprite.color = Color.green;
        }

        if(isDetected==true)
        {
            ShootPlayer();
        }



    }

    private void RotateTowardsPlayer(Vector3 direction)
    {
        float angle=Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        gunTransform.rotation=Quaternion.Euler(0,0,angle);
    }


    private void ShootPlayer()
    {
        if (Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + attackDelay;
            EnemyProjectileView newProjectile = GameService.Instance.EnemyProjectilePool.GetPooledItem();
            newProjectile.SetTransformPosition(muzzleTransform.position);
            newProjectile.SetDamage(Attackdamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void TakeDamage(int damage)
    {
        health-=damage;
        if (this != null)
        {
            StartCoroutine(SetColor());
        }
        if (health<=0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator SetColor()
    {
        mainSprite.color = Color.red;
        yield return new WaitForSeconds(2f);
        if(this!=null)
        {
            detectedSprite.color = Color.white;
        }
        mainSprite.color = Color.white;
    }

}
