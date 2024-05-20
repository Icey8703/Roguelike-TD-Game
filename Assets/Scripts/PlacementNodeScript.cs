using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Manages the placement nodes in the scene
// Each node has one of these scripts
// This stores the placement offset of the tower(dependent on the tower), the tower itself
// and reacts based on the mouse's interaction with the node
public class PlacementNodeScript : MonoBehaviour
{
    // Fields
    public Renderer rend;
    public Color hoveredColor;
    public Color defaultColor;
    private Vector3 placementOffset;
    [SerializeField] private GameObject tower;
    private GameObject towerSchematic;
    TowerPlacementManager placementManager;
    [SerializeField] private GameObject schematicDisplayed;
    [SerializeField] private GameObject rangeDisplayed;
    public GameObject rangeObj;

    // Start is called before the first frame update
    void Start()
    {
        // Grabs the Renderer to allow for the node's material to be altered during runtime 
        rend = GetComponent<Renderer>();
        // Sets the default color to its normal color
        defaultColor = rend.material.color;
        // identifies the placement manager instance
        placementManager = TowerPlacementManager.instance;
        rangeObj = placementManager.rangeObject;

    }

    // when the mouse is clicked on the node
    private void OnMouseDown()
    {

        if (schematicDisplayed != null && rangeDisplayed != null)
        {

            Destroy(schematicDisplayed);
            Destroy(rangeDisplayed);

        }

        // conditional statements to prevent issues with the shop, and being unable to afford the tower
        if (ShopManager.shopInstance.gameObject.activeSelf || placementManager.GetTowerForPlacement() == null)
        {

            return;

        } else if (placementManager.GetPrice() > EconomyManager.instance.bank)
        {

            Debug.Log("cannot afford tower");
            rend.material.color = defaultColor;
            placementManager.SetTowerObject(null, 0, null);
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
        GameObject towerToPlace = placementManager.GetTowerForPlacement();

        // set the placement offset based on the tower(my modeling is wacky)
        if (towerToPlace == placementManager.SniperSentry)
        {

            placementOffset = new Vector3(0f, 1.75f, 0f);

        } else if (towerToPlace == placementManager.GatlingSentry)
        {

            placementOffset = new Vector3(0f, 1.175f, -5f);

        } else if (towerToPlace == placementManager.RocketSentry)
        {

            placementOffset = new Vector3(0f, 2.5f, 0f);

        }

        tower = Instantiate(towerToPlace, transform.position + placementOffset, transform.rotation);
        EconomyManager.instance.makePurchase(placementManager.GetPrice());
        rend.material.color = defaultColor;
        placementManager.SetTowerObject(null, 0, null);

    }

    // whenever the mouse hovers over the node
    private void OnMouseEnter()
    {

        // conditional statements to prevent issues with the shop 
        if (ShopManager.shopInstance.gameObject.activeSelf || placementManager.GetTowerForPlacement() == null)
        {

            return;

        }

        GameObject towerToPlace = placementManager.GetTowerForPlacement();

        if (towerToPlace == placementManager.SniperSentry)
        {

            placementOffset = new Vector3(0f, 1.75f, 0f);

        }
        else if (towerToPlace == placementManager.GatlingSentry)
        {

            placementOffset = new Vector3(0f, 1.175f, -5f);

        }
        else if (towerToPlace == placementManager.RocketSentry)
        {

            placementOffset = new Vector3(0f, 2.5f, 0f);

        }

        towerSchematic = placementManager.GetSchematic();

        schematicDisplayed = Instantiate(towerSchematic, transform.position + placementOffset, transform.rotation);
        rangeDisplayed = Instantiate(rangeObj, transform.position, transform.rotation);
        rangeDisplayed.transform.localScale = new Vector3(towerToPlace.GetComponent<TowerScript>().range * 2, 0.05f, towerToPlace.GetComponent<TowerScript>().range * 2);

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
        
        if (schematicDisplayed != null && rangeDisplayed != null)
        {

            Destroy(schematicDisplayed);
            Destroy(rangeDisplayed);

        }

        // set the material color back to its initial color(navy blue as specified by the material for nodes)
        rend.material.color = defaultColor;

    }
}
