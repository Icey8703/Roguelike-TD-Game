using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// Manages the scaling of time in the scene, will adjust based on the speed set by the user(1x, 1.5x, or 0.5x)
public class TimeScaleScript : MonoBehaviour
{

    public Button button;
    public TextMeshProUGUI text;
    private Queue<float> speedIncrements;

    private void Start()
    {

        button.onClick.AddListener(changeSpeed);
        speedIncrements = new Queue<float>(new[] { 1.0f, 1.5f, 0.5f});
        Application.targetFrameRate = 60;

    }

    void changeSpeed()
    {

        if (Time.timeScale < 1.0f)
        {

            return;

        }

        speedIncrements.Enqueue(speedIncrements.Dequeue());
        Time.timeScale = speedIncrements.Peek();
        text.text = speedIncrements.Peek() + "x\nSpeed";

    }


}
