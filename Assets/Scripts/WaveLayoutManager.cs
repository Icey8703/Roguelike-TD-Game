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
    public Transform minibossEnemy;
    public Transform bossEnemy;
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

        waves = new Wave[] { new Wave(normalEnemy.gameObject, 6, 0.3f), new Wave(normalEnemy.gameObject, 10, 0.2f),
            new Wave(fastEnemy.gameObject, 14, 0.15f), new Wave(fastEnemy.gameObject, 18, 0.15f),
            new Wave(tankyEnemy.gameObject, 10, 0.2f), new Wave(fastEnemy.gameObject, 22, 0.075f),
            new Wave(tankyEnemy.gameObject, 14, 0.1f), new Wave(normalEnemy.gameObject, 20, 0.01f),
            new Wave(minibossEnemy.gameObject, 6, 0.3f), new Wave(bossEnemy.gameObject, 2, 1.0f) };

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

        if (!(waves.Length - 1 < waveNum))
        {

            for (int i = 0; i < waves[waveNum - 1].count; i++)
            {

                SpawnEnemy(waves[waveNum - 1].enemyType);
                yield return new WaitForSeconds(waves[waveNum - 1].spawnRate);

            }

        } else
        {

            for (int i = 0; i < Mathf.CeilToInt(Mathf.Pow(waveNum * 1.75f, UnityEngine.Random.Range(1.1f, 1.25f))); i++)
            {

                int randomEnemyValue = Mathf.CeilToInt(UnityEngine.Random.Range(0.01f, 3f));
                SpawnEnemy((randomEnemyValue == 1 ? normalEnemy.gameObject : 
                    (randomEnemyValue == 2 ? fastEnemy.gameObject : tankyEnemy.gameObject)));
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.075f, 0.15f));

            }

            SpawnEnemy(bossEnemy.gameObject);

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

