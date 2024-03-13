using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{

    public static TowerPlacementManager instance;

    [SerializeField] private GameObject towerObject;
    [SerializeField] private GameObject GatlingSentry;

    private void Awake()
    {
        
        if (instance != null) 
        {

            Debug.Log("A TowerPlacementManager already exists in the current scene");
            return;

        }

        instance = this;

    }

    private void Start()
    {

        towerObject = GatlingSentry;

    }

    public GameObject GetTowerForPlacement()
    {

        return towerObject;

    }

}
