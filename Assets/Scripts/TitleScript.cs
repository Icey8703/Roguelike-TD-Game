using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{

    [SerializeField] private Vector3 m_from = new Vector3(0f, 0f, -10f);
    [SerializeField] private Vector3 m_to = new Vector3(0f, 0f, 10f);
    [SerializeField] private float m_freq = 1f;
    private Transform title;

    private void Awake()
    {

        title = transform;

    }
    private void Update()
    {

        Quaternion from = Quaternion.Euler(m_from);
        Quaternion to = Quaternion.Euler(m_to);

        float lerp = 0.5f * (1.0f + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * m_freq));
        title.rotation = Quaternion.Lerp(from, to, lerp);

    }

}
