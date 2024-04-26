using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffectBehavior : MonoBehaviour
{

    // default timer for destruction
    public float destroyTimer = 1f;

    // Update is called once per frame
    public void Update()
    {
        
        // If the timer is up, destroy the impact effect particle system
        if (destroyTimer <= 0f)
        {

            Destroy(gameObject);

        }

        // subtract the time between the last frame and this frame from the timer
        destroyTimer -= Time.deltaTime;

    }

}
