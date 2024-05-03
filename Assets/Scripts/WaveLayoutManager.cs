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

    public Transform enemyPrefab;
    public Transform spawnPointPart;

    public TextMeshProUGUI WaveCountdownTimer;

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
        enemiesPresent++;

    }

}

