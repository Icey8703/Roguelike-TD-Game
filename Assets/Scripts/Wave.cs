using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{

    public GameObject enemyType;
    public int count;
    public float spawnRate;

    public Wave(GameObject _enemyType, int _count, float _spawnRate)
    {

        // 3125
        enemyType = _enemyType;
        count = _count;
        spawnRate = _spawnRate;
        // inverted containment

    }


}
