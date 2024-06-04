using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
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
    private void Start ()
    {
        rig = GetComponent<Rigidbody2D>();    
    }	
	
    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0) {
            //esta andando
            animator.SetBool("taAndando", true);
        } else {
            //esta parado
            animator.SetBool("taAndando", false);
        }

        if (Input.GetMouseButton(0)) {
            animator.SetBool("taAtacando", true);
        } else {
            animator.SetBool("taAtacando", false);
        }

        if (transform.position.y <= deathZone) {
            Die();
        }
        
        Move();
        Jump();
    }

    private void Move () {
        float inputAxis = Input.GetAxis("Horizontal");
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

        if (!isJumping && rig.velocity.y == 0) {
            animator.SetBool("taPulando", false);
        }
    }

    public void Die() {
        // Adicione aqui o que deve acontecer quando o personagem morre
        Debug.Log("Player morreu!");

        // Exemplo: reiniciar a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
}