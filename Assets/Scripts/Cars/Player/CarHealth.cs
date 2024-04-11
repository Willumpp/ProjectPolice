using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarHealth : MonoBehaviour
{
    public float health = 350f;
    public static bool isDead = false;
    public bool killObject = false;
    public bool invincible = false;
    public static bool policeTouching = false;
    public GameObject destroyedSelf;

    Image healthBar;


    //Audio
    public AudioSource audioSourcePlayerSpawn;

    [HideInInspector]
    public float maxHealth;

    void Start()
    {
        try { healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>(); }
        catch { };
        policeTouching = false;
        isDead = false;
        maxHealth = health;
        audioSourcePlayerSpawn.Play(); //Play spawn sound
    }

    public void Kill()
    {
        if (invincible == false)
        {
            isDead = true;
            Destroy(gameObject);
            Timer.stopTimer = true;
            Time.timeScale = 0.5f; //Slow time for affect
            Instantiate(destroyedSelf, transform.position, transform.rotation); //Spawn destroyed self
        }
    }

    public void DecreaseHealth(float damage)
    {
        if (invincible == false)
        {
            health -= damage;
            healthBar.fillAmount = health / maxHealth; //Update health bar
        }
    }

    void Update()
    {
        //Die if told to or health reaches 0
        if (health <= 0f || killObject == true || Input.GetKey(KeyCode.K))
        {
            Kill();
        }
    }

    public IEnumerator BustedTimer()
    {
        yield return new WaitForSeconds(5f);
        if (policeTouching == true && CountDownTimer.inCountdown == false)
        {
            try { Kill(); }
            catch { };
        }
        yield return null;
    }
}
