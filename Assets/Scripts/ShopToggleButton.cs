using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
