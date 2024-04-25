using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Transform targetEnemy;
    private float projectileSpeed;
    private float damage;
    private float splashRadius;
    public GameObject impactEffect;
    public ParticleSystem moveEffect;

    // this is pretty much a constructor method but it only assigns fields
    public void SeekTarget(Transform _targetEnemy, float _damage, float _projectileSpeed, float _splashRadius)
    {

        targetEnemy = _targetEnemy;
        damage = _damage;
        projectileSpeed = _projectileSpeed;
        splashRadius = _splashRadius;

        // tries to get the particle system component for the move effect
        TryGetComponent<ParticleSystem>(out moveEffect);

    }

    // Update is called once per frame
    private void Update()
    {
        
        // if the enemy is DEAD!!!!! then destroy this object
        if (targetEnemy == null)
        {

            Destroy(gameObject); return;

        }

        // if the move effect exists and it's not playing, start it playing at 10x simulation speed
        if (moveEffect != null && !moveEffect.isPlaying)
        {

            moveEffect.Play();
            var main = moveEffect.main;
            main.simulationSpeed = 10f;

        }

        // figure out the direction from the enemy to the projectile and calculate the distance per frame
        Vector3 direction = targetEnemy.position - transform.position;
        float distanceOnFrame = projectileSpeed * Time.deltaTime;
        /* Quaternion quaternRot = Quaternion.lookRotation(direction);
        Vector3 rotation = quaternRot.eulerAngles;
        transform.rotation = */

        // impact the enemy if the magnitude of the direction is below the distance per frame
        if (direction.magnitude <= distanceOnFrame)
        {

            Impact();
            return;

        }

        
        transform.Translate(direction.normalized * distanceOnFrame, Space.World);

    }

    void Impact()
    {

        if (impactEffect != null)
        {

            GameObject tempObj = (GameObject)Instantiate(impactEffect, targetEnemy.transform.position, targetEnemy.transform.rotation);

        }

        EnemyMovementScript enemyBehavior = targetEnemy.GetComponent<EnemyMovementScript>();

        if (splashRadius > 0f)
        {

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {

                if (Vector3.Distance(transform.position, enemy.transform.position) <= splashRadius)
                {

                    enemyBehavior.TakeDamage(damage);

                }

            }

        } else
        {

            enemyBehavior.TakeDamage(damage);

        }

        
        // possible pierce implementation using a conditional and a field. **make sure it doesn't lock onto the same enemy(s) that it has already hit**
        Destroy(gameObject);

    }


}
