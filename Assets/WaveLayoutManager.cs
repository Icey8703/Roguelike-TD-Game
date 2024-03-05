using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLayoutManager : MonoBehaviour
{

    public Transform enemyPrefab;

    public float waveTimeGap = 5f;
    private float generalCountdown = 2f;
    private string[,] waves;

    private void Awake()
    {

        waves = new string[,] { { "NormalEnemy 6 1" }, { "NormalEnemy 12 0.5" }, { "NormalEnemy 25 0.25" } };

    }

    void Update()
    {

        if (generalCountdown <= 0f)
        {

            SpawnWave();
            generalCountdown = waveTimeGap;

        }

        generalCountdown -= Time.deltaTime;

    }

    void SpawnWave()
    {

        Debug.Log("Wave Spawning");



    }

}

