using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class used for waves of enemies
// only utilized for the first set of waves before it is fully randomized without the class
public class Wave
{

    public GameObject enemyType;
    public int count;
    public float spawnRate;

    public Wave(GameObject _enemyType, int _count, float _spawnRate)
    {

        enemyType = _enemyType;
        count = _count;
        spawnRate = _spawnRate;

    }


}
