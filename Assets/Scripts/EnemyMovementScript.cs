using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Manages how an enemy behaves based on external forces and such im overcomplicating it idk
// Tells it how to move, its health, when it should die(lowtiergod), and has fields necessary for the first targeting :clueless:
public class EnemyMovementScript : MonoBehaviour
{

    // fields/stats
    public float movementSpeed = 5f;
    public float health = 10f;
    private Transform target;
    public int waypointIndex = 0;
    public float distanceToNextWaypoint;
    EconomyManager ecoManager;

    // start is called before the first frame update
    void Start()
    {

        target = Waypoints.waypoints[0];
        ecoManager = EconomyManager.instance;

    }

    // update is called once per frame
    void Update()
    {

        // calculate the direction it must move, the distance to its next waypoint, and translate it toward it
        UnityEngine.Vector3 direction = target.position - transform.position;
        distanceToNextWaypoint = direction.magnitude;
        transform.Translate(direction.normalized * movementSpeed * Time.deltaTime, Space.World);

        // if it's at the waypoint, get the next waypoint to move to
        if (UnityEngine.Vector3.Distance(transform.position, target.position) <= 0.22f)
        {

            GetNextWaypoint();

        }

    }

    // gets the next waypoint, if it's at the last waypoint, destroy the enemy
    void GetNextWaypoint()
    {

        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {

            LifeScript.instance.takeAwayLife(health);
            Destroy(gameObject);
            return;

        }

        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];

    }

    // take damage when hit
    public void TakeDamage(float damageDealt) 
    {

        // if dead, return before it loses more health(and as a result loses the player money)
        if (health <= 0)
        {

            return;

        }

        // if the shot kills the enemy, only gain money based on how much health is remaining
        if (health < damageDealt)
        {

            ecoManager.gainMoney(Mathf.Ceil(health / 2));

        } else // if it doesn't, gain half of the amount of damage dealt as money
        {

            ecoManager.gainMoney(Mathf.Ceil(damageDealt / 2));

        }

        // lower health accordingly
        health -= damageDealt;

        // if the enemy is dead, destroy it
        /* this had to be checked beforehand since sometimes there would be money gained before the
         * enemy was destroyed, giving the player negative money */
        if (health <= 0f) {

            Destroy(gameObject);

        }

    }

}

