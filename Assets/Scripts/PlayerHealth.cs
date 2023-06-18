using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 500;
    public int health;

    public static PlayerHealth instance;
    public TextMeshProUGUI healthText;
 public  PlayerInput input;

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
       ResetHealth();
        healthText.text = $"Health: {health}";
    }

    public void ResetHealth()
    {
        health = startingHealth;
        input.enabled = true;
        dead = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        healthText.text = $"Health: {health}";
       if(health <= 0 && !dead)
        {
            dead = true;
            StartCoroutine(Die());

            GetComponentInChildren<HumanReceivePoint>().SubmitScore(); // and from here we have to fetch it for the top score
            FindObjectOfType<GameCanvas>().GameOver();

        }
    }

    private IEnumerator Die()
    {
        input.enabled = false;
        yield return new WaitForSeconds(1);
       
    }
}
