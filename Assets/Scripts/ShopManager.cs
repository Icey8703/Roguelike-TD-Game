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

    private void Awake()
    {

        if (shopInstance != null)
        {

            Debug.Log("A ShopManager already exists in the scene");
            return;

        }

        shopOptions = new Transform[5];

        for (int i = 1; i <= 5; i++)
        {

            shopOptions[i - 1] = transform.Find("ShopOption" + i);

        }

        shopInstance = this;
        shopInstance.gameObject.SetActive(false);

        commonItemList = new string[] { "Auto\nSear", "HP\nRounds", "Tracking\nModule" };

    }

    public void RefreshShop()
    {

        foreach (Transform option in shopOptions)
        {

            option.gameObject.SetActive(true);
            option.GetComponent<ShopOptionManager>().purchased = false;
            float roll = Random.Range(0f, 100f);

            /*if (roll <= 73.2f)
            {*/

                Transform optionName = option.Find("Name");
                TextMeshProUGUI optionText = optionName.GetComponent<TextMeshProUGUI>();
                int randItemIndex = Mathf.FloorToInt(Random.Range(0, commonItemList.Length));
                optionText.SetText(commonItemList[randItemIndex]);
                option.GetComponent<ShopOptionManager>().currItemID = randItemIndex + 1;
            
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
        
        if (shopInstance != null && shopInstance.gameObject.activeSelf)
        {

            foreach (Transform option in shopOptions)
            {

                option.gameObject.SetActive(!(option.GetComponent<ShopOptionManager>().purchased));

            }

        }

    }

}
