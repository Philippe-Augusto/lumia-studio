using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GordonFrenesi : MonoBehaviour
{
    public int health = 5;
    public float speed;
    private Transform target;
    private bool isFacingRight = false;

    [SerializeField] private Animator animator;

    public HealthSystem playerLife;
    public float attackInterval = 1.0f; // Intervalo de tempo entre ataques
    public float visionRange = 10f; // Alcance da visão
    public LayerMask obstacleLayer; // Camada para detectar obstáculos

    private bool isAttacking = false; // Verifica se está atacando atualmente
    private bool hasSeenPlayer = false; // Verifica se já viu o jogador

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (health <= 0)
        {
            // animação de morte
            animator.SetBool("morreu", true);
            Destroy(gameObject, 1.5f);
        }
        else
        {
            if (CanSeePlayer())
            {
                hasSeenPlayer = true;
            }

            if (hasSeenPlayer)
            {
                Move();
            }
            else
            {
                animator.SetBool("taAndando", false);
            }
        }
    }

    bool CanSeePlayer()
    {
        // Direção do jogador a partir da posição do Gordon
        Vector2 directionToPlayer = target.position - transform.position;

        // Verifica se o jogador está dentro do alcance da visão
        if (directionToPlayer.magnitude <= visionRange)
        {
            // Executa um Raycast para verificar obstruções
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, visionRange, obstacleLayer);

            // Se o Raycast não colidir com nada ou colidir com o jogador, Gordon pode ver o jogador
            if (hit.collider == null || hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    void Move()
    {
        float moveDirection = target.position.x - transform.position.x;

        // Move-se em direção à posição alvo apenas no eixo X
        transform.position = new Vector2(
            Mathf.MoveTowards(transform.position.x, target.position.x, speed * Time.deltaTime),
            transform.position.y
        );

        if (transform.position.x != target.position.x)
        {
            animator.SetBool("taAndando", true);
        }
        else
        {
            animator.SetBool("taAndando", false);
        }

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

    public void TakeDamage()
    {
        health--;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            animator.SetBool("taAtacando", false);
            isAttacking = false;
            StopCoroutine(AttackPlayer());
        }
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        while (isAttacking)
        {
            animator.SetBool("taAtacando", true);
            playerLife.health--;
            yield return new WaitForSeconds(attackInterval);

            // Volta à animação de idle
            animator.SetBool("taAtacando", false);
            yield return new WaitForSeconds(0.5f); // Tempo para voltar à animação de idle
        }
    }
}
