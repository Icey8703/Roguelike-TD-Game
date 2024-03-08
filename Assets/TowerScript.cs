using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerScript : MonoBehaviour
{

    public Transform targetedEnemy;
    public float range = 15.0f;
    

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

    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);

    }

    void UpdateTargetedEnemy()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
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

}
