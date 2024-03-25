using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public static ShopManager shopInstance;
    [SerializeField] private Transform[] shopOptions;
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

            if (Random.Range(0f, 100f) <= 73.2f)
            {

                Transform optionName = option.Find("Name");
                TextMeshProUGUI optionText = optionName.GetComponent<TextMeshProUGUI>();
                optionText.SetText(commonItemList[Mathf.FloorToInt(Random.Range(0, commonItemList.Length))]);

            }

            option.gameObject.SetActive(true);

        }

    }

}
