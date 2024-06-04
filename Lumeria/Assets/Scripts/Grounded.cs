using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour {

    Player player;

    void Start () {
        player = gameObject.transform.parent.gameObject.GetComponent<Player>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            player.isGrounded = true; //o personagem está no chão
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            player.isGrounded = false; //o personagem está no ar
        }
    }
}

