using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;

    public float moveSpeed;

    public bool isGrounded;

    private bool isFacingRight = true;
    public bool isJumping;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;



    private Rigidbody2D rig;

    private void Start ()
    {
        rig = GetComponent<Rigidbody2D>();        
    }	
	
    private void Update()
    {
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
        //Flip();
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

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }

        if (Input.GetButtonUp("Jump") && rig.velocity.y > 0f)
        {
            rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
    }

    private void Flip()
    {
        float inputAxis = Input.GetAxis("Horizontal");
        if (isFacingRight && inputAxis < 0f || !isFacingRight && inputAxis > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
}