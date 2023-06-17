using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;

    public static PlayerHealth instance;
    [SerializeField] TextMeshProUGUI healthText;
  [SerializeField]  PlayerInput input;

    bool dead = false;
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
       if(health < 0 && !dead)
        {
            dead = true;
            StartCoroutine(Die());

            GetComponentInChildren<HumanReceivePoint>().SubmitScore(); // and from here we have to fetch it for the top score

        }
    }

    private IEnumerator Die()
    {
        input.enabled = false;
        yield return new WaitForSeconds(1);
       
    }
}
