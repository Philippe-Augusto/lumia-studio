using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GordonFrenesi : MonoBehaviour
{
    public int health = 5;
    public float speed;
    private Transform target;
    private bool isFacingRight = false;

    public HealthSystem playerLife;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            Destroy(this.gameObject);
        }
        float moveDirection = target.position.x - transform.position.x;

        // Move-se em direção à posição alvo apenas no eixo X
        transform.position = new Vector2(
            Mathf.MoveTowards(transform.position.x, target.position.x, speed * Time.deltaTime),
            transform.position.y
        );

        // Verifica se precisa flipar o sprite
        if (moveDirection > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveDirection < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // Inverte a variável de controle de direção
        isFacingRight = !isFacingRight;

        // Multiplica a escala do inimigo por -1 no eixo X para flipar
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void TakeDamage() {
        health--;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerLife.health--;
        }
    }
}
