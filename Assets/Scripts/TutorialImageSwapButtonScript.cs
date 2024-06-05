using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// swaps the displayed image within the tutorial panel on click
public class TutorialImageSwapButtonScript : MonoBehaviour
{

    public GameObject image1;
    public GameObject image2;

    public void OnClicked()
    {

        image1.SetActive(image2.activeSelf);
        image2.SetActive(!image1.activeSelf);

    }

}
