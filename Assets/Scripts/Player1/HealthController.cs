using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    float startHealthL = 100, startHealthR = 100;
    [SerializeField]
    float currentHealthL, currentHealthR;

    public float damage;
    GameObject healthBarR, healthBarL;
    Image imgR, imgL;

    public PlayerController playerControllerL, playerControllerR;

    
    
    public bool isHit = false;


    // Start is called before the first frame update
    void Start()
    {
        healthBarR = GameObject.Find("HealthbarPlayer2_FG");
        imgR = healthBarR.GetComponent<Image>();
        healthBarL = GameObject.Find("HealthbarPlayer1_FG");
        imgL = healthBarL.GetComponent<Image>();
        currentHealthL = startHealthL;
        currentHealthR = startHealthR;
    }

    // Update is called once per frame
    void Update()
    {
        
        LoseHealth();
        
    }

    void LoseHealth()
    {
        if (playerControllerR.isAttacking == true && isHit == false)
        {
            UpdateHealthBar(damage, imgL, currentHealthL, startHealthL);
            print("tewst");
            isHit = true;
            playerControllerR.isAttacking = false;
        }
        if (playerControllerL.isAttacking == true && isHit == false)
        {
            UpdateHealthBar(damage, imgR, currentHealthR, startHealthR);
            print("tewst");
            isHit = true;
            playerControllerL.isAttacking = false;
        }
    }

    void UpdateHealthBar(float dmg, Image img, float currentHP, float startHP)
    {
        currentHP = currentHP - dmg;
        img.fillAmount =  currentHP / startHP;

    }
}
