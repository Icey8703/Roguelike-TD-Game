using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Starts the game by loading the game scene
public class StartButtonScript : MonoBehaviour
{
    public Button button;

    private void Awake()
    {

        button.onClick.AddListener(startGameplay);

    }

    public void startGameplay()
    {

        SceneManager.LoadScene(1);

    }

}
