using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOptionManager : MonoBehaviour
{

    [SerializeField] private Button shopOption;

    private void Start()
    {

        shopOption.onClick.AddListener(onClicked);
        gameObject.SetActive(false);

    }

    void onClicked()
    {

        transform.gameObject.SetActive(false);

    }


}
