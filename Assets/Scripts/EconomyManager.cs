using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


// This script manages the actual amount of money you have by adding/subtracting, setting the start value, and updating the visual value 
public class EconomyManager : MonoBehaviour
{
    
    // fields
    public static EconomyManager instance;
    private ItemInventoryManager inventories;
    public float bank;
    public BankManager bankText;

    // Awake is called when the script is being loaded
    private void Awake()
    {
        
        // if the instance exists, print a message to console and return
        if (instance != null)
        {

            Debug.Log("Economy manager already exists in the scene");
            return;

        }

        // set the starting cash to 350
        instance = this;
        bank = 350;

    }

    // Start is called before the first frame update
    private void Start()
    {

        // get the bank text script
        bankText = BankManager.instance;
        inventories = ItemInventoryManager.instance;

    }

    // when a purchase is made, update the bank accordingly
    public void makePurchase(float cost)
    {

        bank -= cost;
        bankText.UpdateText("$" + bank.ToString());

    }

    // when gaining money, update the bank accordingly
    public void gainMoney(float money)
    {

        bank += money * (1 + (inventories.GetTowerItems("Gatling")[4][1] * 0.15f));
        bankText.UpdateText("$" + bank.ToString());

    }

}
