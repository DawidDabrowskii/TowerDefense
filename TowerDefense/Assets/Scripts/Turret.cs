using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]

    [SerializeField] private float range = 15f;

    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;

    [SerializeField] private float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;



    private void Start() // makes function check 2 times a second
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget () // search for a target tagged as 'enemy', find closest one, check if closest one is within range
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;  // store shortest distance to enemy 
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            } // lose 'connection' to target if it is outside range
            else
            {
                target = null;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
    }

    private void Update() // if we do not have 'target'. do not do anything
    {
        if (target == null)
            return;

        // lock rotation on enemy as enemy comes withing range
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; // lerp helps with smoothing rotations in this case from 1 enemy to another
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f) ;

        if (fireCountdown <= 0f) // if it reaches 0, shoot, if there is target, 'shoot'
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime; // every second fire countdown is reduced by 1
    }

    private void Shoot()
    {
        GameObject BulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = BulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    private void OnDrawGizmosSelected() // shown range of a turret
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
