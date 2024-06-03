using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages the lives of the user and the loss menu/screen when the player runs out of lives
public class LifeScript : MonoBehaviour
{

    public float lives = 125f;
    public static LifeScript instance;
    [SerializeField] private TextMeshProUGUI textObj;

    private void Awake()
    {

        if (instance != null)
        {

            Debug.Log("life script already exist in scene, ooga booga");
            return;

        }

        instance = this;

    }

    public void takeAwayLife(float amount)
    {

        lives -= amount;

        textObj.text = lives.ToString() + " Lives";

        if (lives <= 0f)
        {

            SceneManager.LoadScene(2);
            return;

        }

    }


}
