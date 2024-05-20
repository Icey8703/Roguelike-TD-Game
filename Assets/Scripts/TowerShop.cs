using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// Manages the specific option in the tower shop
// The buy methods are what tower the button corresponds to, and can be adjusted in the workspace
public class TowerShop : MonoBehaviour
{

    [SerializeField] private TowerPlacementManager towerPlacer;
    [SerializeField] private TextMeshProUGUI tmpGUI;
    public int price;

    private void Start()
    {

        towerPlacer = TowerPlacementManager.instance;

    }

    // When a gatling sentry is selected for purchase
    public void BuyGatling()
    {

        // set the tower object to the gatling sentry for placement
        towerPlacer.SetTowerObject(towerPlacer.GatlingSentry, price, towerPlacer.GatlingSchem);

    }

    public void BuySniper()
    {

        towerPlacer.SetTowerObject(towerPlacer.SniperSentry, price, towerPlacer.SniperSchem);

    }

    public void BuyRocket()
    {

        towerPlacer.SetTowerObject(towerPlacer.RocketSentry, price, towerPlacer.RocketSchem);

    }

    private void Update()
    {

        tmpGUI.text = "$" + price;

    }

}
