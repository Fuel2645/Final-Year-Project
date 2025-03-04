using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    private GameObject foodRef;
    List<GameObject> spawnedFood = new List<GameObject>();
    private float BoundX, BoundZ;
    private int FoodLimit;
    private float Min, Max;


    public void initialise(float gBoundX, float gBoundZ, int MaxFood, float MinDelay, float MaxDelay, GameObject FoodRef)
    {
        print("Food Spawner Start");


        BoundX = gBoundX;
        BoundZ = gBoundZ;
        FoodLimit = MaxFood;
        Min = MinDelay;
        Max = MaxDelay;
        foodRef = FoodRef;

        RandomFoodTimer();
    }



    void RandomFoodTimer()
    {
        Invoke("SpawnFood", Random.Range(Min, Max));
    }

    void SpawnFood()
    {
        float rndX = Random.Range(BoundX * -1, BoundX + 1);
        float rndZ = Random.Range(BoundZ * -1, BoundZ + 1);


        if (spawnedFood.Count < FoodLimit)
        {
            Vector3 spawnLocation = new Vector3(0, 0, 0);
            spawnLocation.x = rndX;
            spawnLocation.z = rndZ;
            spawnedFood.Add(Instantiate(foodRef, spawnLocation, this.transform.rotation));
            spawnedFood.LastOrDefault().GetComponent<FoodData>().FoodValue = Random.Range(10,41);
        }

        RandomFoodTimer();
    }
}
