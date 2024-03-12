using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovementScript : MonoBehaviour
{

    public float movementSpeed = 5f;
    public float health = 10f;
    private Transform target;
    public int waypointIndex = 0;
    public float distanceToNextWaypoint;

    void Start()
    {

        target = Waypoints.waypoints[0];

    }

    void Update()
    {

        Vector3 direction = target.position - transform.position;
        distanceToNextWaypoint = direction.magnitude;
        transform.Translate(direction.normalized * movementSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.22f)
        {

            GetNextWaypoint();

        }

        if (health <= 0f) {

            destroy(this);

        }

    }

    void GetNextWaypoint()
    {

        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {

            Destroy(gameObject);
            return;

        }

        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];

    }

}

