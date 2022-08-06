using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float range = 15f;
    [SerializeField] private float turnSpeed = 10f;

    public string enemyTag = "Enemy";

    public Transform partToRotate;

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
    }
    private void OnDrawGizmosSelected() // shown range of a turret
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
