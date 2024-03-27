using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOptionManager : MonoBehaviour
{

    [SerializeField] private Button shopOption;
    public bool purchased;
    [SerializeField] private ItemInventoryManager inventoryManager;
    public int currItemID;


    private void Awake()
    {

        purchased = true;

    }
    private void Start()
    {

        shopOption.onClick.AddListener(onClicked);
        gameObject.SetActive(false);
        inventoryManager = ItemInventoryManager.instance;

    }

    void onClicked()
    {
        
        inventoryManager.AddItemToTower("all", currItemID);

        purchased = true;
        transform.gameObject.SetActive(false);

    }

}
