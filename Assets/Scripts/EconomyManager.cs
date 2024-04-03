using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    
    public static EconomyManager instance;
    public float bank;
    public BankManager bankText;

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

    private void Start()
    {

        bankText = BankManager.instance;

    }

    public void makePurchase(float cost)
    {

        bank -= cost;
        bankText.UpdateText("$" + bank.ToString());

    }

    public void gainMoney(float money)
    {

        bank += money;
        bankText.UpdateText("$" + bank.ToString());

    }

}
