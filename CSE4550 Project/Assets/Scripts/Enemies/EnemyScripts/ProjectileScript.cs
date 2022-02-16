using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    public Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d.velocity = transform.right * speed;
    }
}
