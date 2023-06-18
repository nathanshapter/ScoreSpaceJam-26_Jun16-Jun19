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

   public bool dead = false;

    [SerializeField] AudioClip damageSound,gameOver;
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
        DisplayHealth();
    }

    public void DisplayHealth()
    {
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
        if(!dead)
        {
            health -= damage;
            AudioManager.instance.PlaySound(damageSound, false);
            healthText.text = $"Health: {health}";
            if (health <= 0 && !dead)
            {
                dead = true;
                StartCoroutine(Die());

               

            }
        }

       
    }

    public IEnumerator Die()
    {
        AudioManager.instance.PlaySound(gameOver, false);
        input.enabled = false;
        yield return new WaitForSeconds(1);
        GetComponentInChildren<HumanReceivePoint>().SubmitScore(); // and from here we have to fetch it for the top score
        FindObjectOfType<GameCanvas>().GameOver();

    }
}
