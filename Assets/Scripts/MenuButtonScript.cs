using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// returns the user to the main menu upon click
public class MenuButtonScript : MonoBehaviour
{

    public void OnClicked()
    {

        SceneManager.LoadScene(0);

    }

}
