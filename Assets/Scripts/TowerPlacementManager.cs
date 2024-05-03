using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the placement of towers, sending a prefab object to the node requesting it
// The price is sent as well, however is provided by the tower shop button itself
public class TowerPlacementManager : MonoBehaviour
{

    // the global instance of the TowerPlacementManager class within in the Director
    public static TowerPlacementManager instance;

    // currently selected towerObject and all of the towers that exist in the game
    [SerializeField] private GameObject towerObject;
    public GameObject GatlingSentry;
    public GameObject SniperSentry;
    public GameObject RocketSentry;
    [SerializeField] private int towerPrice;


    // before the application starts
    private void Awake()
    {

        // if there is already an instance of the TowerPlacementManager, prevent another from being created and print a warning to the console
        if (instance != null) 
        {

            Debug.Log("A TowerPlacementManager already exists in the current scene");
            return;

        }

        // set the TowerPlacementManager instance to this manager
        instance = this;

    }

    public void SetTowerObject(GameObject tower, int price)
    {

        towerObject = tower;
        towerPrice = price;

    }

    // returns the towerObject to be placed on a node(called by placement nodes)
    public GameObject GetTowerForPlacement()
    {

        return towerObject;

    }

    public int GetPrice()
    {

        return towerPrice;

    }

}
