using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class WaveLayoutManager : MonoBehaviour
{

    public Transform enemyPrefab;
    public Transform spawnPointPart;

    public float waveTimeGap = 5f;
    private float generalCountdown = 2f;
    private int waveNum = 1;

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

        for (int i = 0; i < waveNum * 3; i++)
        {

            SpawnEnemy();

        }

        waveNum++;


    }

    void SpawnEnemy()
    {

        Instantiate(enemyPrefab, spawnPointPart.position, spawnPointPart.rotation);

    }

}

