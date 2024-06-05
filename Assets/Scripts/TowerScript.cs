using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

// Manages every aspect of the tower it is assigned to
// Each tower type has adjusted values, however a base set of values is provided here
// Alters the tower's rotation, target, stats(based on items), and dictates its attacks
public class TowerScript : MonoBehaviour
{

    // assorted functionality fields
    [SerializeField] private Transform targetedEnemy;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform rotPart;
    [SerializeField] private GameObject targetingDropdownMenu;
    [SerializeField] private int targetingModeIndex = 0;
    [SerializeField] private Transform mainBody;
    [SerializeField] private ItemInventoryManager inventoryManager;
    [SerializeField] private string towerNameVal = "Gatling";
    [SerializeField] private ParticleSystem MuzzleFlashEffect;
    public int price = 250;
    
    [Header("Stats/Attributes")]

    // stats/attributes fields
    public float range = 15.0f;
    private string[] targetingModes = { "first", "close" };
    public float firerate = 2f;
    public float projectileDamage = 1f;
    public int luck = 0;
    public float projectileSpeed = 80f;
    public float splashRadius = 0f;
    private float firingCountdown = 0f;
    public float baseFireRate;
    public float baseDamage;
    public float baseRange;
    public int baseLuck = 0;
    public float baseProjectileSpeed = 80f;
    public float baseSplashRadius = 0f;

    // Start is called before the first frame update
    void Start()
    {

        price = 250;

        InvokeRepeating("UpdateTargetedEnemy", 0f, 0.025f);
        targetingDropdownMenu = GameObject.Find("TowerTargetingDropdown");
        inventoryManager = ItemInventoryManager.instance;

    }

    // Update is called once per frame
    void Update()
    {

        // update the stats in accordance with items the tower has
        range = baseRange * (1 + (inventoryManager.GetTowerItems(towerNameVal)[2][1] * 0.05f));
        projectileDamage = baseDamage + (0.5f * inventoryManager.GetTowerItems(towerNameVal)[1][1]);
        firerate = baseFireRate * (1 + (inventoryManager.GetTowerItems(towerNameVal)[0][1] * 0.1f));
        luck = baseLuck/* add luck here or something later idk */;
        splashRadius = baseSplashRadius;

        // return if there is no targeted enemy for performance purposes
        if (targetedEnemy == null)
        {

            return;

        }

        // fire if the cooldown is over
        if (firingCountdown <= 0f)
        {

            firingCountdown = 1f / (firerate);
            Attack();

        }

        // rotate the tower to aim at the enemy it's targeting and decrement the cooldown by the time from the last frame to this frame
        UpdateTowerRotation();

        firingCountdown -= Time.deltaTime;
        

    }

    // when selected in the workspace, the tower will create a wire sphere to display its range(for debugging)
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(mainBody.position, range);

    }

    // updates the targeted enemy based on the selected targeting mode
    void UpdateTargetedEnemy()
    {
        

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        
        TMPro.TMP_Dropdown targetingModeDropdown = targetingDropdownMenu.GetComponent<TMPro.TMP_Dropdown>();
        targetingModeIndex = targetingModeDropdown.value;

        // if the targeting mode is set to close, calculate the closest enemy in range
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

        } // if it's set to first, calculate which enemy is the first while within range
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

                if ((enemyWaypoint > firstEnemyWaypoint 
                    && Vector3.Distance(mainBody.position, enemy.transform.position) <= range)
                    || (enemyWaypoint == firstEnemyWaypoint 
                    && enemyWaypointDistance <= firstEnemyWaypointDistance 
                    && Vector3.Distance(mainBody.position, enemy.transform.position) <= range))
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

    // updates the rotation of the tower towards the targeted enemy
    void UpdateTowerRotation()
    {

        // if it is targeting an enemy calculate the direction from the tower to the enemy
        // then create a look rotation using said direction
        // set the look rotation to its euler angles and assign it to a vector3
        // rotate the rotation part, which is assigned in the workspace and controls specific parts of the prefab model
        if (targetedEnemy != null)
        {

            Vector3 enemyDirection = targetedEnemy.transform.position - transform.position;
            Quaternion turretAim = Quaternion.LookRotation(enemyDirection);
            Vector3 rotation = turretAim.eulerAngles;
            rotPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            
        }

    }

    // fires a projecting at an enemy
    void Attack()
    {

        // cancel the attack if there is no enemy in range(avoids NullReferenceExceptions)
        if (targetedEnemy == null)
        {

            return;

        }

        // instantiates the projectile, gets its BulletBehavior script, initializes a boolean for unstable munitions' proc status
        GameObject projectileGameObject = (GameObject)Instantiate(bullet, firePoint.position, firePoint.rotation);
        BulletBehavior projectile = projectileGameObject.GetComponent<BulletBehavior>();
        bool unstableProc = false;

        // if the projectile script exists
        if (projectile != null)
        {

            // if the player owns a copy of unstable munitions
            if (inventoryManager.GetTowerItems(towerNameVal)[3][1] > 0)
            {
                
                // get a random float between 0.0 and 100.0, and if it's <= 10.0, successfully procs unstable(10% chance, might reduce)
                if (UnityEngine.Random.Range(0f, 100f) <= 10f)
                {
                    
                    // proc unstable's boolean, and log the proc to the console for debug purposes
                    unstableProc = true;
                    Debug.Log("Unstable has proc'd");

                }

            }

            // have the projectile fire towards the targeted enemy and assign its damage
            // its damage is affected by the number of HP rounds the player has
            // as well whether unstable munitions has proc'd, also changes by the number the player has
            projectile.SeekTarget(targetedEnemy, projectileDamage * ((unstableProc ? 1 : 0) + inventoryManager.GetTowerItems(towerNameVal)[3][1] + 1), projectileSpeed, splashRadius);
            

            // creates a muzzle flash effect instance at the firing point
            if (MuzzleFlashEffect != null) {

                ParticleSystem tempObj = (ParticleSystem)Instantiate(MuzzleFlashEffect, firePoint);

            }
        }

    }

}
