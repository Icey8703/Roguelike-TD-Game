using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleParent : MonoBehaviour
{
    public static ParticleParent instance;

    private void Start()
    {
        
        if (instance != null)
        {

            Debug.Log("a particle parent already exists in the scene");
            return;

        }

        instance = this;

    }

}
