using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contruction : MonoBehaviour {
    float amountPerClick = 10f;
    
    //Ensure Objects are Referenced           
    GameObject gm;
    GameManager gmScript;
    GameObject shipYard;
    BuildingData buildingDataToUse;
    Timer timer;
    SpaceShipData shipDataToUse;
    ShipYard shipYardData;


    [Header("Populate These Lists In Order of Building/Ship List In documents VERY IMPORTANT")]
    public List<Sprite> shipSprites = new List<Sprite>();
    public List<Sprite> buildingImages = new List<Sprite>();
   
    bool isBuildingShip = false;
    bool isBuildingBuilding = false; 
    

    void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        shipYard = GameObject.Find("ShipYard");
        shipYardData = shipYard.GetComponent<ShipYard>();
    }
    // EVERYTIME WE ADD AN ITEM WE MUST INCLUDE IT IN THIS METHOD WITH THE VALUES ASSIGNED TO EACH ONE GOES HERE
    //IMPORTANT
  public void CreateItem(string name)
    {
        switch (name) { 
            //Creates Scout Ship
            case "Scout":
                SpaceShipData scoutShip = new SpaceShipData();
                scoutShip.SetIron(500);                        
                scoutShip.timeToBuildShip = 10f;
                scoutShip.SetSpeed(10f);
                scoutShip.SetSprite(shipSprites[0]);
                scoutShip.nameOfSpaceShip = "Scout";
                Debug.Log(scoutShip.GetIron());
                SetCurrentShip(scoutShip);
            break;
            //Creates HeadQuarters
            case "HQ":
                BuildingData HQ = new BuildingData();
                HQ.SetIronRequired(500);
                HQ.SetTimeToBuild(20);
                HQ.SetName("HQ");
                HQ.SetSprite(buildingImages[0]);
                SetBuildingToCreate(HQ);
                break;
             }  
       
    }
  void SetCurrentShip(SpaceShipData shipData)
    {
        Debug.Log("Ship Created");
        //TODO Add other Checks To The if Statement
        //Checks To see if Player Has Correct Amount of Resources To Build Ship
        if (GameManager.instance.resourceData.iron >= shipData.ironRequired && !isBuildingShip )
        {      
            shipData.ironRequired = shipData.GetIron();
            float totalRequired = shipData.ironRequired + shipData.glassRequired + shipData.steelRequired;
            shipDataToUse = shipData;
            isBuildingShip = true;
            StartCoroutine(BuildShip());                              
        }
         
    }
    //Method That Creates the ship
    // While loop Is repeated every Second
    // Gets Time To build the ship from the Current Ship That is building
    //TODO Allow Multiple Ships to be constructed
    public IEnumerator BuildShip()
       {
            float timeToBuild = shipDataToUse.timeToBuildShip;
           // timeToBuild -= timer.CompareTime();
            while (timeToBuild >= 0)
            {
            timeToBuild--;
            Debug.Log("THE TIME REMAINING TO BUILD SHIP " + timeToBuild);
            yield return new WaitForSeconds(1f);       
            }
        shipYardData.AddToShipsInYard(shipDataToUse);
        StopCoroutine(BuildShip());
    }
    // Sets Current Building That is being Created
    public void SetBuildingToCreate(BuildingData buildingData)
    {
        Debug.Log("Building Being Built");
        
        float ironRequired = buildingData.GetIronRequired();
        if (ironRequired >= GameManager.instance.resourceData.iron && !isBuildingBuilding)
        {
            
            buildingDataToUse = buildingData;
            GameManager.instance.resourceData.iron -= ironRequired;
            isBuildingBuilding = true;
        }
        else
        {
            Debug.Log("You Either Do not Have Enough Resources To Build This Or There is currently a building Being Built");
        }
    }
    // Build the current Building Over Time
    public IEnumerator BuildBuilding()
    {
        float timeToBuild = buildingDataToUse.GetTimeToBuild();
        timeToBuild -= timer.CompareTime();
        while (timeToBuild >= 0)
        {
            timeToBuild--;
            Debug.Log("THE TIME REMAINING TO BUILD SHIP " + timeToBuild);
            yield return new WaitForSeconds(1f);
        }
        //TODO add to Building LIST
        isBuildingBuilding = false;
        StopCoroutine(BuildBuilding());
    }
}
