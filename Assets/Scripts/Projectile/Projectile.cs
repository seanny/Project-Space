using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float lifeSpan; 

    private void Start()
    {
        StartCoroutine(Deactivate());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(lifeSpan);
        gameObject.SetActive(false);
    }
}
