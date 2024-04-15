using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public static ShopManager shopInstance;
    public Transform[] shopOptions;
    [SerializeField] private string[] commonItemList;
    [SerializeField] private string[] rareItemList;
    [SerializeField] private Dictionary<string, int> itemIDHolder;
    private void Awake()
    {

        // if a global shop manager already exists, prevent a duplicate being created
        if (shopInstance != null)
        {

            Debug.Log("A ShopManager already exists in the scene");
            return;

        }

        // create the shop options array to hold each option in the shop
        shopOptions = new Transform[5];

        // add the shop options to the array for ease of access
        for (int i = 1; i <= 5; i++)
        {

            shopOptions[i - 1] = transform.Find("ShopOption" + i);

        }

        // set the shop instance to this object
        shopInstance = this;
        // deactivate the shop for no obstruction
        shopInstance.gameObject.SetActive(false);

        // initialize the items for each tier
        commonItemList = new string[] { "Auto\nSear", "HP\nRounds", "Tracking\nModule" };
        rareItemList = new string[] { "Unstable\nMunitions" };
        // set up the item ID cheat sheet
        itemIDHolder = new Dictionary<string, int> { {"Auto\nSear", 1}, {"HP\nRounds", 2}, {"Tracking\nModule", 3}, {"Unstable\nMunitions", 4} };

    }

    public void RefreshShop()
    {

        foreach (Transform option in shopOptions)
        {

            // set the options to active, changes their purchased field to false, and rolls a 1 to 100 chance for item choice
            option.gameObject.SetActive(true);
            option.GetComponent<ShopOptionManager>().purchased = false;
            float roll = Random.Range(0f, 100f);

            /*if (roll <= 73.2f)
            {*/

                Transform optionName = option.Find("Name");
                TextMeshProUGUI optionText = optionName.GetComponent<TextMeshProUGUI>();
                int randItemIndex = Mathf.FloorToInt(Random.Range(0, commonItemList.Length));
                optionText.SetText(commonItemList[randItemIndex]);

                if (!itemIDHolder.TryGetValue(commonItemList[randItemIndex], out option.GetComponent<ShopOptionManager>().currItemID)) 
                {

                    Debug.Log("No such item exists in itemIDHolder");
                    return;

                }
            
            /*

            } else if (roll >= 73.2f && roll <= 99.0f)
            {

                Transform optionName = option.Find("Name");
                TextMeshProUGUI optionText = optionName.GetComponent<TextMeshProUGUI>();
                optionText.SetText("Rare item name");

            } else if (roll >= 99.0f) {

                Transform optionName = option.Find("Name");
                TextMeshProUGUI optionText = optionName.GetComponent<TextMeshProUGUI>();
                optionText.SetText("Legendary item name");

            }*/

        }

    }

    public void Update()
    {
        
        // if there IS a shop manager global and its active
        if (shopInstance != null && shopInstance.gameObject.activeSelf)
        {

            // sets the active state of the shop options to the opposite of its purchased state
            foreach (Transform option in shopOptions)
            {

                option.gameObject.SetActive(!(option.GetComponent<ShopOptionManager>().purchased));

            }

        }

    }

}
