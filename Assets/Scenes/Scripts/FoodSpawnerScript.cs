using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public GameObject foodRef;
    List<GameObject> spawnedFood = new List<GameObject>();
    public float radius;
    public int FoodLimit;
    public int Min, Max;

    private void Start()
    {
        SpawnFood();
        
    }

    void RandomFoodTimer()
    {
        Invoke("SpawnFood", Random.Range(Min, Max));
    }

    void SpawnFood()
    {
        float rndX = Random.Range(radius * -1, radius + 1);
        float rndZ = Random.Range(radius * -1, radius + 1);


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
