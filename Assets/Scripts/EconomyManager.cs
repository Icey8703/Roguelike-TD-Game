using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    
    public static EconomyManager instance;
    public int bank;

    private void Awake()
    {
        
        if (instance != null)
        {

            Debug.Log("Economy manager already exists in the scene");
            return;

        }

        instance = this;
        bank = 350;

    }
    
    public void makePurchase(int cost)
    {

        bank -= cost;

    }

}
