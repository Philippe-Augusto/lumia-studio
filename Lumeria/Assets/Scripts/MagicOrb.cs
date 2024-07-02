using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOrb : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 6.0f; // Velocidade de movimento do objeto mágico
    private Vector3 moveDirection;  // Direção de movimento do orbe
    GordonFrenesi gordon;
    [SerializeField] private Transform positionOrb;

    void Update()
    {
        Debug.Log("Valor do Move Direction: " + moveDirection);
        positionOrb.position += moveDirection * speed * Time.deltaTime;
        Destroy(gameObject, 4f);
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
