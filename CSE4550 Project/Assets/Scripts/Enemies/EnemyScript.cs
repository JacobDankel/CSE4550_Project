using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D rb2d;
    [SerializeField]
    protected Transform tran;
    [SerializeField]
    protected BoxCollider2D boxCollider;
    [SerializeField]
    protected LayerMask groundLayer, damageLayer;
    [SerializeField]
    protected float speed = 2f, direction = 1, health = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        tran = GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        patrol();
        takeDamage(1);
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.y);
    }

    protected void takeDamage(float damage)
    {
        if (boxCollider.IsTouchingLayers(damageLayer))
        {
            health -= damage;
        }
        if (health == 0)
        {
            Debug.Log("goomba ded");
            Destroy(gameObject);
        }
    }

    protected void patrol()
    {
        if (atCorner())
        {
            direction *= -1;
        }
    }

    protected bool atCorner()
    {
        Vector3 leftExtent = boxCollider.bounds.center + Vector3.left * boxCollider.bounds.extents.x; //left center point of boxCollider
        Vector3 rightExtent = boxCollider.bounds.center + Vector3.right * boxCollider.bounds.extents.x; //right center point of boxCollider
        float extra = 2f; //so that the third parameter is just outside of the collider, *** source of glitchiness if not tuned correctly.

        RaycastHit2D lHit = Physics2D.Raycast(leftExtent, Vector2.down, leftExtent.y + extra, groundLayer);
        RaycastHit2D rHit = Physics2D.Raycast(rightExtent, Vector2.down, rightExtent.y + extra, groundLayer);

        float left = -.01f;
        float right = .01f;

        if (!lHit.collider && direction <= left)
        {
            return true;
        }
        if (!rHit.collider && direction >= right)
        {
            return true;
        }
        else return false;
    }
}
