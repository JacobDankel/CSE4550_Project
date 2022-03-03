using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().pickUpItem(this);
            Destroy(gameObject);
        }
    }
}
