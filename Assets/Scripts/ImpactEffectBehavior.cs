using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffectBehavior : MonoBehaviour
{

    public float destroyTimer = 1f;

    public void Update()
    {
        
        if (destroyTimer <= 0f)
        {

            Destroy(gameObject);

        }

        destroyTimer -= Time.deltaTime;

    }

}
