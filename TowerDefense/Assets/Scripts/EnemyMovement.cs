using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;

    private void Start()
    {
        target = Waypoints.points[0];
    }

    private void Update() // target moves toward waypoint
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) // target moves until in reaches waypoint (0.2f)
        {
            GetNextWayPoint();
        }
    }

    private void GetNextWayPoint() // moving target to next waypoint
    {
        if (wavepointIndex >= Waypoints.points.Length - 1) // moving until there are no more waypoints and then destroy
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex]; // next target is wavepointIndex ++
    }
}
