using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Transform targetEnemy;
    public float projectileSpeed = 80f;

    public void SeekTarget(Transform _targetEnemy)
    {

        targetEnemy = _targetEnemy;


    }

    private void Update()
    {
        
        if (targetEnemy == null)
        {

            Destroy(this); return;

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

        Debug.Log("impact");

    }


}
