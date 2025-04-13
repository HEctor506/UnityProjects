using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class MoveSpaceShip : MonoBehaviour
{
    private Rigidbody2D rb;
    private float x_dir;
    private float moveSpeed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //gets my actual rb
        
    }
    //What is the difference between FixedUpdate and Update???

    void FixedUpdate()
    {
        x_dir = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;
        rb.linearVelocity = new Vector2(x_dir, 0f); 
    }
}
