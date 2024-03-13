using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementNodeScript : MonoBehaviour
{

    public Renderer rend;
    public Color hoveredColor;
    public Color defaultColor;
    private Vector3 placementOffset;
    [SerializeField] private GameObject tower;

    // Start is called before the first frame update
    void Start()
    {
        
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;
        placementOffset = new Vector3(0f, 1.175f, 0f);

    }

    private void OnMouseDown()
    {
        
        if (tower != null)
        {

            Debug.Log("this node is already taken(implement a gui alert later)");
            return;

        }

        GameObject towerToPlace = TowerPlacementManager.instance.GetTowerForPlacement();
        Instantiate(towerToPlace, transform.position + placementOffset, transform.rotation);
        tower = towerToPlace;

    }

    private void OnMouseEnter()
    {

        rend.material.color = hoveredColor;

    }

    private void OnMouseExit()
    {
        
        rend.material.color = defaultColor;

    }
}
