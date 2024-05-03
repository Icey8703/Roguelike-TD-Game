using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Manages the shop's active state by detecting when the button to toggle it is pressed
// if the shop panel is active after being toggled it sets all of the options to active,
// then if the option is already purchased, it will set it inactive again
public class ShopToggleButton : MonoBehaviour
{

    // fields and initialization of a listener
    [SerializeField] private Button shopToggle;
    [SerializeField] private GameObject shopPanel;
    private void Start()
    {

        shopToggle.onClick.AddListener(toggleShop);

    }

    // toggles the shop
    void toggleShop()
    {

        // if the shop panel exists then set the activity to the opposite of what it was
        if (shopPanel != null)
        {

            shopPanel.SetActive(!(shopPanel.activeSelf));

            // if the shop panel is active, check each option's purchased state and set activity accordingly
            if (shopPanel.activeSelf)
            {

                foreach (Transform option in ShopManager.shopInstance.shopOptions)
                {

                    option.gameObject.SetActive(true);

                    if (option.GetComponent<ShopOptionManager>() != null && option.GetComponent<ShopOptionManager>().purchased)
                    {

                        option.gameObject.SetActive(false);
                        Debug.Log(option.name + " set inactive");

                    }

                }

            }
            
            

        }
        

    }

}
