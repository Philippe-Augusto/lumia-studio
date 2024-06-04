using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{    
    public int maxHealth;

    public int health;
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
