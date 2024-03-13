using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public static ShopManager shopInstance;

    private void Awake()
    {

        if (shopInstance != null)
        {

            Debug.Log("A ShopManager already exists in the scene");
            return;

        }

        shopInstance = this;

    }


}
