using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class WaveLayoutManager : MonoBehaviour
{

    public Transform enemyPrefab;
    public Transform spawnPointPart;

    public TextMeshProUGUI WaveCountdownTimer;

    public float waveTimeGap = 30f;
    private float generalCountdown = 2f;
    private int waveNum = 1;
    private bool allSpawned = true;

    void Update()
    {

        if (generalCountdown <= 0f)
        {

            allSpawned = false;
            StartCoroutine(SpawnWave());
            generalCountdown = waveTimeGap;

        }

        if (allSpawned)
        {

            generalCountdown -= Time.deltaTime;
            WaveCountdownTimer.text = Mathf.CeilToInt(generalCountdown).ToString();

        }

        

    }

    IEnumerator SpawnWave()
    {

        for (int i = 0; i < Math.Ceiling(Math.Pow(Math.E, waveNum) / 5) + 2; i++)
        {

            SpawnEnemy();
            yield return new WaitForSeconds(0.3f);

        }

        allSpawned = true;

        waveNum++;


    }

    void SpawnEnemy()
    {

        Instantiate(enemyPrefab, spawnPointPart.position, spawnPointPart.rotation);

    }

}

