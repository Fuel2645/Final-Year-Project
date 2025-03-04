using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationStarter : MonoBehaviour
{
    public int HerbivourCount;
    public GameObject HerbivourRef;
    public float HerbivourSpeed;
    public int HerbivourHealth;

    public int CarnivourCount;
    public GameObject CarnivourRef;
    public float CarnivourSpeed;
    public int CarnivourHealth;

    public int FoodSpawnerCount;
    public GameObject FoodSpawnerRef;
    public float MinDelay, MaxDelay;
    public int MaxFoodCount;
    public GameObject FoodRef;

    public GameObject CorpseRef;
    public float BoundX;
    public float BoundZ;

    private List<GameObject> HerbivourList;
    private List<GameObject> CarnivourList;
    private List<GameObject> FoodSpawnerList;



    private void Start()
    {
     


        Invoke("Test", 0.1f);
    }


    void Test()
    {
        Vector3 whereToSpawn = transform.position;
        for (int i = 0; i < HerbivourCount; i++)
        {
            whereToSpawn.x = UnityEngine.Random.Range(-BoundX, BoundX + 1);
            whereToSpawn.z = UnityEngine.Random.Range(-BoundZ, BoundZ + 1);
            whereToSpawn.y = 0;
            Instantiate(HerbivourRef, whereToSpawn, this.transform.rotation);
        }

        for (int i = 0; i < CarnivourCount; i++)
        {
            whereToSpawn.x = UnityEngine.Random.Range(-BoundX, BoundX + 1);
            whereToSpawn.z = UnityEngine.Random.Range(-BoundZ, BoundZ + 1);
            whereToSpawn.y = 0;
            Instantiate(CarnivourRef, whereToSpawn, this.transform.rotation);
        }


        for (int i = 0; i < FoodSpawnerCount; i++)
        {
            whereToSpawn.x = UnityEngine.Random.Range(-BoundX, BoundX + 1);
            whereToSpawn.z = UnityEngine.Random.Range(-BoundZ, BoundZ + 1);
            whereToSpawn.y = 0;
            Instantiate(FoodSpawnerRef, whereToSpawn, this.transform.rotation);
        }

        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Herbivore"))
        {
            gameObject.GetComponent<herbivorStuff>().initialise(BoundX, BoundZ, HerbivourHealth, HerbivourSpeed, CorpseRef);
        }



        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Carnivore"))
        {
            gameObject.GetComponent<CarnivoreScript>().initialise(BoundX, BoundZ, CarnivourHealth, CarnivourSpeed, CorpseRef);
        }
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("FoodSpawner"))
        {
            gameObject.GetComponent<FoodScript>().initialise(BoundX, BoundZ, MaxFoodCount, MaxDelay, MinDelay, FoodRef);
        }
    }

}
