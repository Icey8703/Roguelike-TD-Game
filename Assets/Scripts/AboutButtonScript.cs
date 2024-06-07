using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// toggles the about panel's active state on click
public class AboutButtonScript : MonoBehaviour
{

    public GameObject aboutPanel;

    public void OnClicked()
    {

        aboutPanel.SetActive(!aboutPanel.activeSelf);

    }

}
