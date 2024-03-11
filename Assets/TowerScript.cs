using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerScript : MonoBehaviour
{

    public Transform targetedEnemy;
    public float range = 15.0f;
    public float turnSpeed = 10f;
    private string[] targetingModes = { "first", "close" };
    private int targetingModeIndex = 0;



    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("UpdateTargetedEnemy", 0f, 0.1f);

    }

    // Update is called once per frame
    void Update()
    {

        if (targetedEnemy == null)
        {

            return;

        }

        UpdateTowerRotation();

    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);

    }

    void UpdateTargetedEnemy()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (targetingModes[targetingModeIndex].Equals("close"))
        {

            float closestEnemyDistance = Mathf.Infinity;
            GameObject closest = null;
            foreach (GameObject enemy in enemies)
            {

                float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);

                if (enemyDistance < closestEnemyDistance)
                {
                    closestEnemyDistance = enemyDistance;
                    closest = enemy;

                }

            }

            if (closest != null && closestEnemyDistance <= range)
            {

                targetedEnemy = closest.transform;

            }
            else if (closestEnemyDistance >= range)
            {

                targetedEnemy = null;

            }

        }
        else if (targetingModes[targetingModeIndex].Equals("first"))
        {
            float firstEnemyWaypoint = Mathf.NegativeInfinity;
            float firstEnemyWaypointDistance = Mathf.Infinity;
            GameObject first = null;
            

            foreach(GameObject enemy in enemies)
            {

                EnemyMovementScript enemyScript = enemy.GetComponent<EnemyMovementScript>();
                int enemyWaypoint = enemyScript.waypointIndex;
                float enemyWaypointdistance = Vector3.Magnitude(enemy.transform.position - Waypoints.waypoints[enemyWaypoint].position);

                if (enemyWaypoint > firstEnemyWaypoint && Vector3.Distance(transform.position, first.transform.position) <= range)
                {

                    first = enemy;
                    firstEnemyWaypoint = enemyWaypoint;
                    firstEnemyWaypointDistance = enemyWaypointdistance;

                }
                else if (enemyWaypoint == firstEnemyWaypoint && enemyWaypointdistance <= firstEnemyWaypointDistance && Vector3.Distance(transform.position, enemy.transform.position) <= range)
                {

                    first = enemy;
                    firstEnemyWaypoint = enemyWaypoint;
                    firstEnemyWaypointDistance = enemyWaypointdistance;

                }
                

            }
            
            if (first != null && Vector3.Distance(transform.position, first.transform.position) <= range)
            {

                targetedEnemy = first.transform;

            } else if (Vector3.Distance(transform.position, first.transform.position) >= range)
            {

                first = null;

            }

        }
        

    }

    void UpdateTowerRotation()
    {

        if (targetedEnemy != null)
        {

            Vector3 enemyDirection = targetedEnemy.transform.position - transform.position;
            Quaternion turretAim = Quaternion.LookRotation(enemyDirection);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, turretAim, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            



        }

    }

}
