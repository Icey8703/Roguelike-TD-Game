using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOptionManager : MonoBehaviour
{

    // fields and such
    [SerializeField] private Button shopOption;
    public bool purchased;
    [SerializeField] private ItemInventoryManager inventoryManager;
    public int currItemID;
    public float price;

    // initialize the purchased state to true so it refreshes at the start
    private void Awake()
    {

        purchased = true;

    }

    // set up the other things necessary for functionality(inventory manager, click listener)
    // also deactivate the game object
    private void Start()
    {

        shopOption.onClick.AddListener(onClicked);
        gameObject.SetActive(false);
        inventoryManager = ItemInventoryManager.instance;

    }


    // when clicked add the item to the tower and deactivate the option, purchased state is now true
    void onClicked()
    {
        
        inventoryManager.AddItemToTower("all", currItemID);

        purchased = true;
        transform.gameObject.SetActive(false);

    }

}
