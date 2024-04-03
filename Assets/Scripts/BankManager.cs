using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class BankManager : MonoBehaviour
{
    
    public static BankManager instance;
    [SerializeField] private TextMeshProUGUI TMPAsset;

    private void Awake()
    {
        
        if (instance != null)
        {

            Debug.Log("Bank Manager already exists in the scene");
            return;

        }

        instance = this;

    }

    public void UpdateText(string updText)
    {
        TMPAsset.text = updText;

    }

}
