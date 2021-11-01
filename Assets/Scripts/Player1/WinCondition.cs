using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    public HealthController hpController;
    public PlayerController playerController;
    public int deathHP = 0;
    public Text text;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Win();
    }
    void Win()
    {
        if (hpController.currentHealth <= deathHP)
        {
            text.text = playerController.navn + " tabte";
        }
    }
}
