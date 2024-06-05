using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// manages the tutorial panel's active state on click
public class TutorialButtonScript : MonoBehaviour
{

    public GameObject tutorialPanel;

    public void OnClicked()
    {

        tutorialPanel.SetActive(!tutorialPanel.activeSelf);

    }
}
