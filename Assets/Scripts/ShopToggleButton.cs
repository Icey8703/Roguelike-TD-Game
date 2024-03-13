using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopToggleButton : MonoBehaviour
{

    [SerializeField] private Button shopToggle;
    [SerializeField] private GameObject shopPanel;
    private void Start()
    {

        shopToggle.onClick.AddListener(toggleShop);

    }

    void toggleShop()
    {
        if (shopPanel != null)
        {

            shopPanel.SetActive(!(shopPanel.activeSelf));

        }
        

    }

}
