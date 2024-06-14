using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public HealthSystem heart;

    private float damageInterval = 1.3f; // Intervalo entre cada dano recebido
    private float damageTimer;
    private bool playerInContact = false; // Indica se o personagem está em contato com o espinho
    private bool canTakeDamage = true; // Indica se o personagem pode receber dano no momento

    void Start()
    {
        damageTimer = damageInterval; // Inicializar o temporizador de dano
    }

    void Update()
    {
        if (playerInContact && canTakeDamage)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0)
            {
                TakeDamage();
                damageTimer = damageInterval; // Resetar o temporizador de dano
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInContact = true;
            damageTimer = 0; // Garante que o dano seja aplicado imediatamente
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInContact = false;
            canTakeDamage = true; // Resetar para permitir dano na próxima colisão
        }
    }

    void TakeDamage()
    {
        heart.health -= 1;
        canTakeDamage = false; // Impedir mais danos temporariamente após um dano
        StartCoroutine(ResetDamageCooldown());
    }

    IEnumerator ResetDamageCooldown()
    {
        yield return new WaitForSeconds(damageInterval);
        canTakeDamage = true; // Permitir dano novamente após o intervalo
    }
}
