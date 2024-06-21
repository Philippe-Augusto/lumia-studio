using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOrb : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 4.0f; // Velocidade de movimento do objeto mágico
    private Vector3 moveDirection;  // Direção de movimento do orbe
    GordonFrenesi gordon;
    void Update()
    {
        // Movimenta o objeto na direção horizontal
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;  // Normalizar para garantir que a direção tenha magnitude 1
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Gordon"))
        {
            gordon = collider.gameObject.GetComponent<GordonFrenesi>();
            // Exemplo: Destruir o objeto se colidir com um inimigo
            gordon.TakeDamage();
            Destroy(gameObject);
        }
    }
}
