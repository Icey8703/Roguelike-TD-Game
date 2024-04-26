using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class BankManager : MonoBehaviour
{
    
    // fields
    public static BankManager instance;
    [SerializeField] private TextMeshProUGUI TMPAsset;

    // Awake is called when the script is being loaded
    private void Awake()
    {
        
        // if the instance is already initialized, log a message in the console and return
        if (instance != null)
        {

            Debug.Log("Bank Manager already exists in the scene");
            return;

        }

        instance = this;

    }

    // update the text for the bank tmpro element(assigned in workspace)
    public void UpdateText(string updText)
    {
        TMPAsset.text = updText;

    }

}
