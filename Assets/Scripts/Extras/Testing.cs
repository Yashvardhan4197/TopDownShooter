using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Testing : MonoBehaviour,IDamageAble
{
    [SerializeField] int health;

    public async void TakeDamage(int damage)
    {
        health-=damage;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        await Task.Delay(1 * 100);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
