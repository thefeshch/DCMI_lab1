using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed = 100f;

    [SerializeField] private float attackPerSecond = 2;
    private float attackRate => 1/ attackPerSecond;
    private float attackCooldown;


    void Update()
    {
        attackCooldown -= Time.deltaTime;
        
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        if (attackCooldown <= 0)
        {
            attackCooldown = attackRate;
            GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }

}

