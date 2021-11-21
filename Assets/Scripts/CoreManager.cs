using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreManager : MonoBehaviour
{
    public GameManager GameManager;

    public float health = 1000f;
    public Text coreHP;

    bool AbleToRegenerate = false;
    float RegenerationRate = 1f;
    public void DealDamage(float damage)
    {
        health -= damage;
        coreHP.text = health.ToString();

        if (health <= 0)
        {
            GameManager.GameDefeat();
        }

        AbleToRegenerate = false;
        Invoke("Regenerate", 5f);
    }

    void Regenerate()
    {
        AbleToRegenerate = true;
    }

     void Update()
    {
        if (AbleToRegenerate == true)
        {
            health += RegenerationRate * Time.deltaTime;
            coreHP.text = (Mathf.Floor(health)).ToString();
        }
    }
}
