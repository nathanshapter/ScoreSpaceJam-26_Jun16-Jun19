using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 500;
   

    public int health;
   
   public bool canTakedmg = true;

    public static PlayerHealth instance;
    public TextMeshProUGUI healthText;
 public  PlayerInput input;

   public bool dead = false;

    [SerializeField] AudioClip damageSound,gameOver;
    [SerializeField] Image healthSlider;
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
        health = startingHealth;

    }

    public void Invincibility(float timer)
    {
        canTakedmg= false;
        StartCoroutine(ResetInvincibility(timer));
    }
    private IEnumerator ResetInvincibility(float timer)
    {
        yield return new WaitForSeconds(timer);
        canTakedmg= true;
    }
    public void DisplayHealth()
    {
        

        healthText.text = $"{health}/{startingHealth}";
       
        healthSlider.fillAmount = health/startingHealth;
    }

    public void ResetHealth()
    {
        startingHealth = 500;
        health = startingHealth;
      
        input.enabled = true;
        dead = false;
    }
    public void TakeDamage(int damage)
    {
        if(!dead && canTakedmg)
        {
            health -= damage;
            AudioManager.instance.PlaySound(damageSound, false);
            DisplayHealth();
            if (health <= 0 && !dead)
            {
                dead = true;
                FindObjectOfType<UpgradeMenu>().SwitchMenu(false);
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
