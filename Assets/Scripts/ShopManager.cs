using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public static ShopManager shopInstance;
    [SerializeField] private Transform[] shopOptions;

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

    }

    public void RefreshShop()
    {

        foreach (Transform option in shopOptions)
        {

            option.gameObject.SetActive(true);

        }

    }

}
