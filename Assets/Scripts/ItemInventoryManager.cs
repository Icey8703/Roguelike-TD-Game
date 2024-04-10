using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ItemInventoryManager : MonoBehaviour
{

    public static ItemInventoryManager instance;

    /* Dictionary for the tower inventories
     * The string correlates to the tower name
     * The 2d int array correlates to the item's ID and how many you have */
    [SerializeField] private Dictionary<string, int[][]> towerInventories;
    private string[] towerNames;
    private int[][] defaultItemCounts;

    private void Awake()
    {

        // checks if there is already an inventory manager in the scene
        if (instance != null)
        {

            // prevents initialization of the global inventory manager
            Debug.Log("an ItemInventoryManager instance already exists in the scene");
            return;

        }

        // if there is none, initialize things
        instance = this;
        towerNames = new string[]{ "Gatling", "Sniper" };
        defaultItemCounts = new int[][]{ (new int[] { 1, 0 }), (new int[] { 2, 0 }), (new int[] { 3, 0 })};

    }

    // Start is called before the first frame update
    void Start()
    {
        
        // initializing the dictionary
        towerInventories = new Dictionary<string, int[][]>();

        foreach (string tower in towerNames)
        {

            towerInventories.Add(tower, defaultItemCounts);

        }

    }

    // Update is called once per frame
    void Update()
    {
        


    }

    // returns the items a tower has
    public int[][] GetTowerItems(string towerName)
    {

        // tries to return it, catches any failure(KeyNotFoundException :steamhappy:)
        try {

            return towerInventories[towerName];

        } catch (Exception e) {

            Debug.Log("inventory does not exist for provided tower name. Exception: " + e);
            return null;

        }

    }

    // adds an item of the given id to the tower givenn
    public void AddItemToTower(string towerName, int itemID)
    {
        
        // if all towers are to gain the item, then loop through all towers and give them the item
        if (towerName.Equals("all"))
        {

            foreach (string tower in towerNames)
            {

                towerInventories[tower][itemID - 1][1]++;
                return;

            }

        }

        // increment the count of items of said ID(just an int that corresponds to the item's order in the code)
        towerInventories[towerName][itemID - 1][1]++;

    }

}
