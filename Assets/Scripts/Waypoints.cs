using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains an array that holds all the waypoints
// very simple class used exclusively for enemy movement paths
public class Waypoints : MonoBehaviour
{

    public static Transform[] waypoints;

    void Awake()
    {

        waypoints = new Transform[transform.childCount];

        for (int i = 0; i < waypoints.Length; i++)
        {

            waypoints[i] = transform.GetChild(i);

        }

    }

}

