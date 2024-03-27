using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
