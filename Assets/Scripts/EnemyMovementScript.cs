using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovementScript : MonoBehaviour
{

    public float movementSpeed = 5f;
    public float health = 10f;
    private Transform target;
    public int waypointIndex = 0;
    public float distanceToNextWaypoint;
    EconomyManager ecoManager;

    void Start()
    {

        target = Waypoints.waypoints[0];
        ecoManager = EconomyManager.instance;

    }

    void Update()
    {

        UnityEngine.Vector3 direction = target.position - transform.position;
        distanceToNextWaypoint = direction.magnitude;
        transform.Translate(direction.normalized * movementSpeed * Time.deltaTime, Space.World);

        if (UnityEngine.Vector3.Distance(transform.position, target.position) <= 0.22f)
        {

            GetNextWaypoint();

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

    public void TakeDamage(float damageDealt) 
    {

        if (health <= 0)
        {

            return;

        }

        if (health < damageDealt)
        {

            ecoManager.gainMoney(health);

        } else
        {

            ecoManager.gainMoney(damageDealt);

        }

        health -= damageDealt;

        if (health <= 0f) {

            Destroy(gameObject);

        }

    }

}

