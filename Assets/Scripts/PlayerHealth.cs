using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;

    public static PlayerHealth instance;
    [SerializeField] TextMeshProUGUI healthText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else { Destroy(this.gameObject); }
    }
    private void Start()
    {
        healthText.text = $"Health: {health}";
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        healthText.text = $"Health: {health}";
       if(health < 0)
        {
            GetComponentInChildren<HumanReceivePoint>().SubmitScore(); // and from here we have to fetch it for the top score

        }
    }
}
