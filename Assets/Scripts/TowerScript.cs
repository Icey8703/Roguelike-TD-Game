using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TowerScript : MonoBehaviour
{

    [SerializeField] private Transform targetedEnemy;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform rotPart;
    [SerializeField] private GameObject targetingDropdownMenu;
    [SerializeField] private int targetingModeIndex = 0;
    [SerializeField] private Transform mainBody;
    [SerializeField] private ItemInventoryManager inventoryManager;
    [SerializeField] private string towerNameVal = "Gatling";
    [SerializeField] private int autoSearCount = 0;
    
    [Header("Stats/Attributes")]

    public float range = 15.0f;
    private string[] targetingModes = { "first", "close" };
    public float firerate = 2f;
    public float projectileDamage = 1f;
    private float firingCountdown = 0f;

    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("UpdateTargetedEnemy", 0f, 0.025f);
        targetingDropdownMenu = GameObject.Find("TowerTargetingDropdown");
        inventoryManager = ItemInventoryManager.instance;

    }

    // Update is called once per frame
    void Update()
    {

        range = 15.0f * (1 + (inventoryManager.GetTowerItems(towerNameVal)[2][1] * 0.05f));
        projectileDamage = 1 + (0.5f * inventoryManager.GetTowerItems(towerNameVal)[1][1]);
        autoSearCount = inventoryManager.GetTowerItems(towerNameVal)[0][1];

        if (firingCountdown <= 0f)
        {

            Attack();
            firingCountdown = 1f / (firerate * (1 + (autoSearCount * 0.1f)));

        }

        if (targetedEnemy == null)
        {

            return;

        }

        UpdateTowerRotation();

        firingCountdown -= Time.deltaTime;
        

    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(mainBody.position, range);

    }

    void UpdateTargetedEnemy()
    {
        

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        
        TMPro.TMP_Dropdown targetingModeDropdown = targetingDropdownMenu.GetComponent<TMPro.TMP_Dropdown>();
        targetingModeIndex = targetingModeDropdown.value;

        if (targetingModes[targetingModeIndex].Equals("close"))
        {

            float closestEnemyDistance = Mathf.Infinity;
            GameObject closest = null;
            foreach (GameObject enemy in enemies)
            {

                float enemyDistance = Vector3.Distance(mainBody.position, enemy.transform.position);

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
            float firstEnemyWaypoint = -1;
            float firstEnemyWaypointDistance = Mathf.Infinity;
            float firstEnemyDistance = Mathf.Infinity;
            GameObject first = null;
            

            foreach(GameObject enemy in enemies)
            {

                EnemyMovementScript enemyScript = enemy.GetComponent<EnemyMovementScript>();
                int enemyWaypoint = enemyScript.waypointIndex;
                float enemyWaypointDistance = Vector3.Distance(enemy.transform.position, Waypoints.waypoints[enemyWaypoint].position);

                if ((enemyWaypoint > firstEnemyWaypoint && Vector3.Distance(mainBody.position, enemy.transform.position) <= range)
                    || (enemyWaypoint == firstEnemyWaypoint && enemyWaypointDistance <= firstEnemyWaypointDistance && Vector3.Distance(mainBody.position, enemy.transform.position) <= range))
                {

                    first = enemy;
                    firstEnemyWaypoint = enemyWaypoint;
                    firstEnemyWaypointDistance = enemyWaypointDistance;
                    firstEnemyDistance = Vector3.Distance(mainBody.position, enemy.transform.position);

                }
                

            }
            
            if (first != null && firstEnemyDistance <= range)
            {

                targetedEnemy = first.transform;

            } else if (firstEnemyDistance >= range)
            {

                
                targetedEnemy = null;

            }

        }
        

    }

    void UpdateTowerRotation()
    {

        if (targetedEnemy != null)
        {

            Vector3 enemyDirection = targetedEnemy.transform.position - transform.position;
            Quaternion turretAim = Quaternion.LookRotation(enemyDirection);
            Vector3 rotation = turretAim.eulerAngles;
            rotPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            



        }

    }

    void Attack()
    {

        GameObject projectileGameObject = (GameObject)Instantiate(bullet, firePoint.position, firePoint.rotation);
        BulletBehavior projectile = projectileGameObject.GetComponent<BulletBehavior>();
        
        if (projectile != null)
        {

            // Implement items **later** but this should be a decent way to implement them
            projectile.SeekTarget(targetedEnemy, projectileDamage/* + (2 * damageItemCount)*/, 80);

        }

    }

}
