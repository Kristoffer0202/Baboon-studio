using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    float startHealth = 100;
    [SerializeField]
    public float currentHealth;

    public float damage;
    Image img;
    public Image healthBar;

    public PlayerController playerController;

    public bool isHit = false;


    // Start is called before the first frame update
    void Start()
    {
        img = healthBar.GetComponent<Image>();
        currentHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {  
        LoseHealth();
    }

    void LoseHealth()
    {
        if (playerController.isAttacking == true && isHit == false)
        {
            UpdateHealthBar(damage, img, currentHealth, startHealth);
            isHit = true;
            playerController.isAttacking = false;
        }
    }

    void UpdateHealthBar(float dmg, Image img, float currentHP, float startHP)
    {
        currentHealth = currentHP - dmg;
        img.fillAmount =  currentHP / startHP;
    }
}
