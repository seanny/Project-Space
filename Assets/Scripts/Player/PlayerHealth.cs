using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerCombat pc;
    public int HealthCount;
    private int MaxHealth;
    private SpriteRenderer spr;
    public GameObject HealthHud;

    private void Awake()
    {
        pc = GetComponent<PlayerCombat>();
        spr = GetComponent<SpriteRenderer>();
        MaxHealth = HealthCount;
    }

    public void TakeDamage(int damage)
    {
        HealthCount -= damage;

        UpdateHearts();
        if (HealthCount <= 0) Die();
        else // Damaged but not dead
        {
            StartCoroutine(DamageBlink());
        }
    }


    IEnumerator DamageBlink()
    {
        for (int i = 0; i < 1; i++)
        {

        spr.color = Color.red;
        yield return new WaitForSecondsRealtime(0.25f);
        spr.color = Color.white;
        yield return new WaitForSecondsRealtime(0.25f);
        }
    }

    void UpdateHearts()
    {
        if (HealthHud == null) HealthHud = GameObject.FindGameObjectWithTag("HeathHud");
        List<GameObject> Hearts = new List<GameObject>();
        for (int i = 0; i < HealthHud.transform.childCount; i++)
        {
            Hearts.Add(HealthHud.transform.GetChild(i).gameObject);

        }
        switch (HealthCount)
        {
            case 0:
                Hearts[0].GetComponent<Animator>().SetTrigger("DamagedFull");
                break;
            case 1:
                Hearts[0].GetComponent<Animator>().SetTrigger("DamagedHalf");
                break;
            case 2:
                Hearts[1].GetComponent<Animator>().SetTrigger("DamagedFull");
                break;
            case 3:
                Hearts[1].GetComponent<Animator>().SetTrigger("DamagedHalf");
                break;
            case 4:
                Hearts[2].GetComponent<Animator>().SetTrigger("DamagedFull");
                break;
            case 5:
                Hearts[2].GetComponent<Animator>().SetTrigger("DamagedHalf");
                break;
            default:
                break;
        }
    }

    void Die()
    {

    }
}
