using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleScript : MonoBehaviour
{

    public Button button;
    public TextMeshProUGUI text;
    private Queue<float> speedIncrements;

    private void Start()
    {

        button.onClick.AddListener(changeSpeed);
        speedIncrements = new Queue<float>(new[] { 1.0f, 1.5f, 2.0f});

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
