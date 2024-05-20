using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

// Manages the shop's options and item distribution
// Refreshes every 5 waves, price scaling based on the wave number
public class ShopManager : MonoBehaviour
{

    // fields
    public static ShopManager shopInstance;
    public Transform[] shopOptions;
    [SerializeField] private string[] commonItemList;
    [SerializeField] private string[] rareItemList;
    [SerializeField] private Dictionary<string, int> itemIDHolder;
    [SerializeField] private Dictionary<int, string> itemDescHolder;
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
        rareItemList = new string[] { "Unstable\nMunitions", "Tax\nFraud"};
        // set up the item ID cheat sheet
        itemIDHolder = new Dictionary<string, int> { { "Auto\nSear", 1 }, { "HP\nRounds", 2 }, { "Tracking\nModule", 3 }, { "Unstable\nMunitions", 4 }, { "Tax\nFraud", 5 } };
        // set up item description cheat sheet
        // FINISH THIS BROOOOOO YOU IDIOT YOU ABSOLUTE STUPID IDIOT MORON
        itemDescHolder = new Dictionary<int, string> { { 1, "+10% attack speed per stack" }, { 1, "+0.5 flat damage per stack" }, { 3, "+5% range per stack" }, { 4, "" } };

    }

    public void RefreshShop()
    {

        foreach (Transform option in shopOptions)
        {
            ShopOptionManager optionScript = option.GetComponent<ShopOptionManager>();

            // set the options to active, changes their purchased field to false, and rolls a 1 to 100 chance for item choice
            option.gameObject.SetActive(true);
            optionScript.purchased = false;
            float roll = Random.Range(0f, 100f);

            // if rolling a 73.2% chance, common item, otherwise it's a rare item
            if (roll <= 73.2f)
            {

                Transform optionName = option.Find("Name");
                Transform optionDesc = option.Find("Description");
                TextMeshProUGUI optionNameText = optionName.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI optionDescText = optionDesc.GetComponent<TextMeshProUGUI>();
                int randItemIndex = Mathf.FloorToInt(Random.Range(0, commonItemList.Length));
                optionNameText.SetText(commonItemList[randItemIndex]);

                // try to get the value of commonitemlist at the index randItemIndex
                if (!itemIDHolder.TryGetValue(commonItemList[randItemIndex], out optionScript.currItemID)) 
                {

                    Debug.Log("No such item exists in itemIDHolder");
                    return;

                }

                // set the price using an exponential scaling formula(50 base)
                optionScript.price = Mathf.Ceil(Mathf.Pow(WaveLayoutManager.instance.waveNum, 1.025f) * 50);
                option.Find("Price").GetComponent<TextMeshProUGUI>().text = optionScript.price.ToString();
            

            } else // if (roll >= 73.2f && roll <= 99.0f)
            {

                Transform optionName = option.Find("Name");
                TextMeshProUGUI optionNameText = optionName.GetComponent<TextMeshProUGUI>();
                int randItemIndex = Mathf.FloorToInt(Random.Range(0, rareItemList.Length));
                optionNameText.SetText(rareItemList[randItemIndex]);

                // try to get the value of rareitemlist at the index randItemIndex
                if (!itemIDHolder.TryGetValue(rareItemList[randItemIndex], out option.GetComponent<ShopOptionManager>().currItemID))
                {

                    Debug.Log("No such item exists in itemIDHolder");
                    return;

                }

                // set the price using an exponential scaling formula(75 base)
                option.GetComponent<ShopOptionManager>().price = Mathf.Ceil(Mathf.Pow(WaveLayoutManager.instance.waveNum, 1.025f) * 75);
                option.Find("Price").GetComponent<TextMeshProUGUI>().text = optionScript.price.ToString();

            }/* else if (roll >= 99.0f) {

                Transform optionName = option.Find("Name");
                TextMeshProUGUI optionText = optionName.GetComponent<TextMeshProUGUI>();
                optionText.SetText("Legendary item name");

            }*/

        }

    }

    // Update is called once per frame
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
