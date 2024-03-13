using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Transform targetEnemy;
    private float projectileSpeed = 80f;
    private float damage = 1f;

    public void SeekTarget(Transform _targetEnemy) 
    {

        targetEnemy = _targetEnemy;

    }

    public void SeekTarget(Transform _targetEnemy, float _damage, float _projectileSpeed)
    {

        targetEnemy = _targetEnemy;
        damage = _damage;
        projectileSpeed = _projectileSpeed;

    }

    private void Update()
    {
        
        if (targetEnemy == null)
        {

            Destroy(gameObject); return;

        }

        Vector3 direction = targetEnemy.position - transform.position;
        float distanceOnFrame = projectileSpeed * Time.deltaTime;

        if (direction.magnitude <= distanceOnFrame)
        {

            Impact();
            return;

        }

        
        transform.Translate(direction.normalized * distanceOnFrame, Space.World);

    }

    void Impact()
    {

        EnemyMovementScript enemyBehavior = targetEnemy.GetComponent<EnemyMovementScript>();
        enemyBehavior.TakeDamage(damage);
        // possible pierce implementation using a conditional and a field. **make sure it doesn't lock onto the same enemy(s) that it has already hit**
        Destroy(gameObject);

    }


}
