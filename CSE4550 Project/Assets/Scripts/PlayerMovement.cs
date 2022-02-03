using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform tran;
    [SerializeField]
    private Rigidbody2D controller;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float speed = 4f, jumpForce = 10f, jumpCushion = 1f, jumpTime = .35f;

    private float jumpTimeCounter;
    private bool isJumping;

    private float horizontalMove;
    // Start is called before the first frame update
    void Start()
    {
        tran = GetComponent<Transform>();
        controller = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        //initial jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            controller.velocity = Vector2.up * jumpForce * Time.deltaTime;
        }

        //if jump is held down, then player will continue to rise up to a certain point indicated by jumpTimeCounter
        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                controller.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        controller.velocity = new Vector2(horizontalMove * speed, controller.velocity.y);
    }

    /*
     * Uses raycast to test if the transform is on the ground. 
     * jumpCushion is to allow you to jump slightly earlier than sprite hits the ground. It is the length of the raycast from the center of the object (tran.position)
     * groundLayer is a Layermask so that the Raycast only hits colliders in the "Ground" layer
     */
    private bool IsGrounded()
    {
        
        //IMPORTANT: Currently does not work with ledges. probably need to have two raycasts for either side and use || to determine if one is true, its late and too tired now - tim
        RaycastHit2D hit = Physics2D.Raycast(tran.position, Vector2.down, jumpCushion, groundLayer);
        if (hit.collider != null)
        {
            //Debug.Log("raycast True");
            return true;
        }
        //Debug.Log("raycast False");
        return false;
    }
}
