using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExploreManager : MonoBehaviour {

    private float distanceToExplore = 300;
    private float distance;
    private float timeToExplore;  

    private Asteroid asteroidScript;
   
    private AsteroidData asteroidToUse = new AsteroidData();

    [Header("This List contains all the sprites each asteroid will use")]
    public List<Sprite> spritesOfAsteroids;

    void Start()
    {
        asteroidScript = GameObject.Find("Canvas").gameObject.GetComponent<Asteroid>();
    }
    //Ensure That the GO of the Shipyard That is selected will be passed here IMPORTANT
   public  void StartExplore(SpaceShipData shipToUse)
    {
        if (shipToUse!=null)
        {
            distance = distanceToExplore;
            timeToExplore = shipToUse.GetSpeed() / distanceToExplore;
            distanceToExplore *= .20f;
            StartCoroutine(Explore());
        }
       
    }
    public IEnumerator Explore()
    {
       
        while (timeToExplore >= 0)
        {
            timeToExplore--;     
            yield return new WaitForSeconds(1f);
        }  
        CreateAsteroid(distance);
        StopCoroutine(Explore());
    }
    //Creates Asteroid called when Explore Method Is Finished based on chance
	public void CreateAsteroid(float distanceExplored)
    {
        float minAsteroidTypes = 1f;
        float maxAsteroidTypes = 101f;
        float chooseAsteroidType = Random.Range(minAsteroidTypes, maxAsteroidTypes);
        
        if (chooseAsteroidType >= 1 && chooseAsteroidType < 50)
        {
            AsteroidData ironAsteroid = new AsteroidData();
            ironAsteroid.asteroidType = "Iron";
            ironAsteroid.amountOfResources = distanceExplored * 2000;
            //Index 0 is IRON
            ironAsteroid.spriteToUse = spritesOfAsteroids[0];
            asteroidScript.AddToAsteroidList(ironAsteroid); 
        }
       else if (chooseAsteroidType >= 51 && chooseAsteroidType < 101)
        {
            AsteroidData goldAsteroid = new AsteroidData();
            goldAsteroid.asteroidType = "Gold";
            goldAsteroid.amountOfResources = distanceExplored * 4000;
            //Index 1 is Gold
            ironAsteroid.spriteToUse = spritesOfAsteroids[1];
            asteroidScript.AddToAsteroidList(goldAsteroid);

        }
    }
  
}
