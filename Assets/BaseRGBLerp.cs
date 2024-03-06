using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour

{
    MeshRenderer cubeMeshRenderer;
    [SerializeField][Range(0f, 1f)] float lerpTime;
    [SerializeField] Color[] colors;
    float t = 0f;
    int index = 0;
    int length;

    // Start is called before the first frame update
    void Start()
    {
        
        cubeMeshRenderer = GetComponent<MeshRenderer>();
        length = colors.Length;

    }

    // Update is called once per frame
    void Update()
    {

        cubeMeshRenderer.material.color = Color.Lerp(cubeMeshRenderer.material.color, colors[index], lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        
        if (t > 0.9f)
        {

            t = 0f;
            index++;
            index = (index >= length) ? 0 : index;

        }



    }
}
