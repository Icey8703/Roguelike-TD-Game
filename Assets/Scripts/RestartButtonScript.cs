using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// loads the game scene when clicked
public class RestartButtonScript : MonoBehaviour
{
    public void OnClicked()
    {

        SceneManager.LoadScene(1);

    }
}
