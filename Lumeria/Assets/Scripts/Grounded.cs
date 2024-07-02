using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    Player player;
    public Transform groundCheck;
    public LayerMask groundLayer;

    void Start()
    {
        player = gameObject.transform.parent.gameObject.GetComponent<Player>();
    }

    void Update() {
        player.isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.05f, groundLayer);
    }

    /*void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 3) // Verifica se o collider que entrou é da layer 3 (ground)
        {
            player.isGrounded = true; // O personagem está no chão
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 3) // Verifica se o collider que saiu é da layer 3 (ground)
        {
            player.isGrounded = false; // O personagem não está mais no chão
        }
    }*/
}
