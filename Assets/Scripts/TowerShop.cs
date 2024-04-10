using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
        towerPlacer.SetTowerObject(towerPlacer.GatlingSentry, price);


    }

    public void BuySniper()
    {

        towerPlacer.SetTowerObject(towerPlacer.SniperSentry, price);

    }

    private void Update()
    {

        tmpGUI.text = "$" + price;

    }

}
