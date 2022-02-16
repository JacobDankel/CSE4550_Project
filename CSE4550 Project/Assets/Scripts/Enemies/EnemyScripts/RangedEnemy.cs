using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyClass
{
    [SerializeField]
    private Transform projectileSpawn;
    [SerializeField]
    private GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        patrol();
        shoot();
    }

    private void FixedUpdate()
    {
        moveRB();
    }

    private void shoot()
    {
        if (Input.GetButtonDown("Fire1") || seesPlayer())
        {
            Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
        }
    }
}
