using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;

    public float moveSpeed;

    public bool isGrounded;
    
    private Rigidbody2D rig;

    void Start ()
    {
        rig = GetComponent<Rigidbody2D>();        
    }	
	
    private void Update()
    {
        Move();
        Jump();
    }

    void Move () {        
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        float inputAxis = Input.GetAxis("Horizontal");

        if (inputAxis > 0) {
            transform.eulerAngles = new Vector2(0f, 0f);
        }

        else if (inputAxis < 0){
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }    

    void Jump () {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}