using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int pontosDeMagia;
    public Transform spawnPoint;
    public MagicOrb magicOrbPrefab;
    public int deathZone = 3;
    public float jumpForce;
    public float moveSpeed;
    public bool isGrounded;
    public bool isJumping;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    public Animator animator;
    private Rigidbody2D rig;
    public float glideForce = 2.0f; // A força de planar
    public float glideGravityScale = 1.5f; // Escala de gravidade durante o planar
    private float originalGravityScale;
    private bool isAttacking = false;
    private void Start ()
    {
        rig = GetComponent<Rigidbody2D>();    
        originalGravityScale = rig.gravityScale;
    }	
	
    private void Update()
    {
        if (Input.GetMouseButton(0)) {
            Attack();
        }
        if (transform.position.y <= deathZone) {
            Die();
        }

        Move();
        Jump();
    }

    private void Move () {
        float inputAxis = Input.GetAxis("Horizontal");

        if (inputAxis != 0) {
            //esta andando
            animator.SetBool("taAndando", true);
        } else {
            //esta parado
            animator.SetBool("taAndando", false);
        }
        
        Vector3 movement = new Vector3(inputAxis, 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        if (inputAxis > 0) {
            transform.eulerAngles = new Vector2(0f, 0f);
        }

        else if (inputAxis < 0){
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }    

    private void Jump () {

        //Coyote time
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            isJumping = false; // Reinicializa o estado de salto se estiver no chão
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //Input Buffer
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0 && !isJumping) {
            rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetBool("taPulando", true);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }

        if (Input.GetButtonUp("Jump") && rig.velocity.y > 0f)
        {
            rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        if (Input.GetButton("Jump") && rig.velocity.y < 0f && !isGrounded) { //ela esta no ar, e esta caindo
            Glide();
        } else {
            StopGliding();
        }

        if (!isJumping && rig.velocity.y == 0) {
            animator.SetBool("taPulando", false);
        }
    }

    void Glide()
    {
        rig.AddForce(Vector2.up * glideForce, ForceMode2D.Force);
        rig.gravityScale = glideGravityScale; // Reduzir a gravidade
        animator.SetBool("taPlanando", true);
    }

    void StopGliding()
    {
        rig.gravityScale = originalGravityScale; // Restaurar a gravidade original
        animator.SetBool("taPlanando", false);
    }

    public void Die() {
        // Adicione aqui o que deve acontecer quando o personagem morre
        Debug.Log("Player morreu!");

        // Exemplo: reiniciar a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ColetarPontosDeMagia() {
        Debug.Log("Ponto de Magia Coletado!");
        pontosDeMagia++;
    }

    void Attack()
    {
        if (!isAttacking && isGrounded && pontosDeMagia > 0)
        {
            pontosDeMagia--;
            isAttacking = true;
            animator.SetBool("taAtacando", true);

            // Cria a orbe mágica
            MagicOrb magicOrb = Instantiate(magicOrbPrefab, spawnPoint.position, spawnPoint.rotation);

            Vector3 direcaoLancamento;

            if (transform.localScale.x > 0) {
                direcaoLancamento = Vector3.right;
            } else {
                direcaoLancamento = Vector3.left;
            }

            magicOrb.SetDirection(direcaoLancamento);
            // Define um tempo de espera antes de permitir outro ataque
            Invoke("ResetAttack", 0.5f); // Ajuste o tempo conforme necessário
        }
        else if (pontosDeMagia <= 0) {
            Debug.Log("Pontos de Magia Insuficientes");
        }
    }

    void ResetAttack()
    {
        animator.SetBool("taAtacando", false);
        isAttacking = false;
    }
    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
}