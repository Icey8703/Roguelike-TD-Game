using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

// Manages the waves, which will be edited later to have actual structure
// The enemies have exponentially scaling group sizes, based on wave number(not balanced but its still too easy)
// Tells the shop to reset every 5 waves
public class WaveLayoutManager : MonoBehaviour
{

    public static int enemiesPresent = 0;

    public Transform normalEnemy;
    public Transform fastEnemy;
    public Transform tankyEnemy;
    public Transform spawnPointPart;

    public TextMeshProUGUI WaveCountdownTimer;
    [SerializeField] private Wave[] waves;

    public static WaveLayoutManager instance;

    public float waveTimeGapScale = 15f;
    private float generalCountdown = 2f;
    public int waveNum = 1;
    [SerializeField] private int refreshCountdown = 1;
    private bool allSpawned = false;

    private void Start()
    {
        
        if (instance != null)
        {

            Debug.Log("Wave layout manager already exists in the scene");
            return;

        }

        waves = new Wave[] { new Wave(normalEnemy.gameObject, 6, 0.3f), new Wave(normalEnemy.gameObject, 10, 0.2f), new Wave(fastEnemy.gameObject, 8, 0.35f), new Wave(fastEnemy.gameObject, 12, 0.15f), new Wave(tankyEnemy.gameObject, 9, 0.2f)};

        instance = this;

    }

    void Update()
    {

        if (generalCountdown <= 0f)
        {

            refreshCountdown--;

            allSpawned = false;
            if (ShopManager.shopInstance != null && refreshCountdown <= 0)
            {

                ShopManager.shopInstance.RefreshShop();
                refreshCountdown = 5;

            }
            StartCoroutine(SpawnWave());
            generalCountdown = Mathf.Floor((waveTimeGapScale * waveNum) / Mathf.Sqrt(waveNum * 2));

        }


        generalCountdown -= Time.deltaTime;
        WaveCountdownTimer.text = Mathf.CeilToInt(generalCountdown).ToString();

        if ((GameObject.FindGameObjectsWithTag("Enemy") == null || GameObject.FindGameObjectsWithTag("Enemy").Length == 0) && allSpawned)
        {

            generalCountdown = 5f;
            allSpawned = false;

        }

    }

    IEnumerator SpawnWave()
    {

        for (int i = 0; i < waves[waveNum - 1].count; i++)
        {

            SpawnEnemy(waves[waveNum - 1].enemyType);
            yield return new WaitForSeconds(waves[waveNum - 1].spawnRate);

        }

        allSpawned = true;
        waveNum++;

    }

    void SpawnEnemy(GameObject enemy)
    {

        Instantiate(enemy, spawnPointPart.position, spawnPointPart.rotation);
        enemiesPresent++;

    }

}

