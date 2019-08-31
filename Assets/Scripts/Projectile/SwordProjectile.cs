using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    [Header("Damage")]
    public int damage;

    [Header("LifeSpam")]
    public float lifeSpan;

    private void Start()
    {
        StartCoroutine(Deactivate());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.ReceiveDamage(damage);

            gameObject.SetActive(false);
        }
    }
    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(lifeSpan);
        gameObject.SetActive(false);
    }
}
