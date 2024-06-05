using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// toggles the about me panel's active state on click
public class AboutMeButtonScript : MonoBehaviour
{

    public GameObject aboutMePanel;

    public void OnClicked()
    {

        aboutMePanel.SetActive(!aboutMePanel.activeSelf);

    }

}
