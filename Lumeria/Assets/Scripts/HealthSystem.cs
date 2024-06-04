using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour
{    
    public int maxHealth;
    public int health;

    public Image[] hearth;
    public Sprite cheio;
    public Sprite vazio;
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceberDano() 
    {
        health--;

        if (health <= 0) {
            Debug.Log("Game Over");
        }
    }
}
