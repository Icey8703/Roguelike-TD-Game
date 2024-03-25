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

        if (instance != null)
        {

            Debug.Log("an ItemInventoryManager instance already exists in the scene");
            return;

        }

        instance = this;
        towerNames = new string[]{ "Gatling" };
        defaultItemCounts = new int[][]{ (new int[] { 1, 0 }) };

    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void AddItemToTower(string towerName, int itemID)
    {
        
        // increment the count of items of said ID(just an int that corresponds to the order in which it was added)
        towerInventories[towerName][itemID - 1][1]++;

    }

}
