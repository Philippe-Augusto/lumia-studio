using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class HealthSystem : MonoBehaviour
{    
    public int maxHealth;
    public int health;
    public Image[] heart;
    public Sprite cheio;
    public Sprite vazio;
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthLogic();
        DeadState();
    }

    void HealthLogic() {
        if (health > maxHealth) {
            health = maxHealth;
        }

        for (int i = 0; i < heart.Length; i++)
        {
            if (i < health) {
                heart[i].sprite = cheio;
            }
            else {
                heart[i].sprite = vazio;
            }

            if (i < maxHealth) {
                heart[i].enabled = true;
            }
            else {
                heart[i].enabled = false;
            }
        }
    }

    void DeadState() {
        if (health <= 0) {
            GetComponent<Player>().enabled = false;
            Destroy(gameObject, 1.0f);
        }
    }
}
