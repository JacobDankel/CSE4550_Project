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
    private BoxCollider2D boxCollider;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    public float speed = 4f, jumpForce = 10f, jumpCushion = 1f, jumpTime = .35f;

    private float jumpTimeCounter;
    private bool isJumping;
    private Animator anim;
    private float horizontalMove;
    // Start is called before the first frame update
    private void Start()
    {
        //Grab references for rigidbody and animator from body 
        tran = GetComponent<Transform>();
        controller = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal");
        jumping();

        /*
        //Set animator parameters
        anim.SetBool("RunCharacter", horizontalMove != 0);
        {

        }
        */
    }



    void FixedUpdate()
    {
        controller.velocity = new Vector2(horizontalMove * speed, controller.velocity.y);
    }


    /*
    * Lets the player hold the button longer to jump higher. tap jump button = shorter jump.
    */
    void jumping()
    {
        //initial jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            controller.velocity = Vector2.up * jumpForce * Time.deltaTime;
        }

        //if jump is held down, then player will continue to rise up to a certain point indicated by jumpTimeCounter which is initialized by jumpTime
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

    /*
        * Uses two raycasts on either side of the collider to test if it is on the ground. 
        * jumpCushion is to allow you to jump slightly earlier than sprite hits the ground.
        * groundLayer is a Layermask so that the Raycast only hits colliders in the "Ground" layer
        */
    bool IsGrounded()
    {
        Vector3 leftExtent = boxCollider.bounds.center + Vector3.left * boxCollider.bounds.extents.x; //left center point of boxCollider
        Vector3 rightExtent = boxCollider.bounds.center + Vector3.right * boxCollider.bounds.extents.x; //right center point of boxCollider

        RaycastHit2D lHit = Physics2D.Raycast(leftExtent, Vector2.down, jumpCushion, groundLayer);
        RaycastHit2D rHit = Physics2D.Raycast(rightExtent, Vector2.down, jumpCushion, groundLayer);
        if (lHit.collider != null || rHit.collider != null)
        {
            return true;
        }
        return false;
    }

    
}
