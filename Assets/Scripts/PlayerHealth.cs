using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else { Destroy(this.gameObject); }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
       if(health < 0)
        {
            GetComponentInChildren<HumanReceivePoint>().SubmitScore(); // and from here we have to fetch it for the top score

        }
    }
}
