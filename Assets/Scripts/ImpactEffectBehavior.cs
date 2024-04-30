using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// manages the effects that occur upon bullet impact
// for example it destroys the explosion effect after x amount of time
public class ImpactEffectBehavior : MonoBehaviour
{

    // default timer for destruction
    public float destroyTimer = 1f;

    // Update is called once per frame
    public void Update()
    {
        
        // make it play if it's somehow not
        if (!GetComponent<ParticleSystem>().isPlaying)
        {

            GetComponent<ParticleSystem>().Play();

        }

        // If the timer is up, destroy the impact effect particle system
        if (destroyTimer <= 0f)
        {

            Destroy(gameObject);

        }

        // subtract the time between the last frame and this frame from the timer
        destroyTimer -= Time.deltaTime;

    }

}
