using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementNodeScript : MonoBehaviour
{
    // Fields
    public Renderer rend;
    public Color hoveredColor;
    public Color defaultColor;
    private Vector3 placementOffset;
    [SerializeField] private GameObject tower;
    TowerPlacementManager placementManager;

    // Start is called before the first frame update
    void Start()
    {
        // Grabs the Renderer to allow for the node's material to be altered during runtime 
        rend = GetComponent<Renderer>();
        // Sets the default color to its normal color
        defaultColor = rend.material.color;
        // Sets up the placement offset
        placementOffset = new Vector3(0f, 1.175f, -5f);
        // identifies the placement manager instance
        placementManager = TowerPlacementManager.instance;

    }

    // when the mouse is clicked on the node
    private void OnMouseDown()
    {

        if (ShopManager.shopInstance.gameObject.activeSelf)
        {

            return;

        }

        if (placementManager.GetTowerForPlacement() == null)
        {

            return;

        }

        if (placementManager.GetPrice() > EconomyManager.instance.bank)
        {

            Debug.Log("cannot afford tower");
            rend.material.color = defaultColor;
            placementManager.SetTowerObject(null, 0);
            return;

        }

        // if the tower slot is already taken(it isn't null)
        if (tower != null)
        {

            // prevent placement of a tower with a console output(gui later) and empty return
            Debug.Log("this node is already taken(implement a gui alert later)");
            return;

        }

        // if the tower field is null, get the tower for placement from the Director
        GameObject towerToPlace = TowerPlacementManager.instance.GetTowerForPlacement();
        // instantiate the tower on the node with the offset
        Instantiate(towerToPlace, transform.position + placementOffset, transform.rotation);
        // Set the tower field so towers can't be stacked
        tower = towerToPlace;
        EconomyManager.instance.makePurchase(placementManager.GetPrice());
        rend.material.color = defaultColor;
        placementManager.SetTowerObject(null, 0);

    }

    // whenever the mouse hovers over the node
    private void OnMouseEnter()
    {

        if (ShopManager.shopInstance.gameObject.activeSelf)
        {

            return;

        }

        if (placementManager.GetTowerForPlacement() == null)
        {

            return;

        }

        // set the material color to the specified color(red as specified in the properties)
        rend.material.color = hoveredColor;

    }

    // whenever the mouse exist the node after hovering over it
    private void OnMouseExit()
    {

        if (ShopManager.shopInstance.gameObject.activeSelf)
        {

            return;

        }

        if (placementManager.GetTowerForPlacement() == null)
        {

            return;

        }

        // set the material color back to its initial color(navy blue as specified by the material for nodes)
        rend.material.color = defaultColor;

    }
}
