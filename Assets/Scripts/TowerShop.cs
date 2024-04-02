using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShop : MonoBehaviour
{

    [SerializeField] private TowerPlacementManager towerPlacer;
    public int price;

    private void Start()
    {

        price = 1;
        towerPlacer = TowerPlacementManager.instance;

    }

    // When a gatling sentry is selected for purchase
    public void BuyGatling()
    {

        // set the tower object to the gatling sentry for placement
        towerPlacer.SetTowerObject(towerPlacer.GatlingSentry, price);


    }

}
