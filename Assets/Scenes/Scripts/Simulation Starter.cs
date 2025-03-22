using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    public float MinFoodDelay, MaxFoodDelay;
    public int MaxFoodCount;
    public GameObject FoodRef;

    public int initialWaterCount;
    public GameObject WaterRef;
    public int MinWaterDelay, MaxWaterDelay;
    public int MaxWaterCount;

    public GameObject CorpseRef;
    public float BoundX;
    public float BoundZ;

    private List<GameObject> WaterList;
    private UnityEngine.Vector3 FoodCheck, Watercheck;


    private void Start()
    {
        WaterList = new List<GameObject>();
        FoodCheck = new UnityEngine.Vector3(3,3,3);
        Watercheck = new UnityEngine.Vector3(15, 1,15);
        Invoke("WaterSpawn", 0.1f);
        
    }


    void WaterSpawn()
    {
        UnityEngine.Vector3 whereToSpawn = transform.position;
        UnityEngine.Quaternion waterRotation = this.transform.rotation;
        waterRotation.x = 180;

        for (int i = 0; i < initialWaterCount; i++)
        {
            whereToSpawn.x = UnityEngine.Random.Range(-BoundX, BoundX + 1);
            whereToSpawn.z = UnityEngine.Random.Range(-BoundZ, BoundZ + 1);
            whereToSpawn.y = 0;
            
            WaterList.Add(Instantiate(WaterRef, whereToSpawn, waterRotation));
            WaterList.LastOrDefault().GetComponent<WaterMesh>().Moving();
        }


        Invoke("RandomWaterSpawn", 0.1f);
    }


    private void RandomWaterSpawn()
    {
        UnityEngine.Vector3 whereToSpawn = transform.position;
        UnityEngine.Quaternion waterRotation = this.transform.rotation;
        for (int i = 0; i < HerbivourCount; i++)
        {
            whereToSpawn.x = UnityEngine.Random.Range(-BoundX / 2, (BoundX / 2) + 1);
            whereToSpawn.z = UnityEngine.Random.Range(-BoundZ / 2, (BoundZ / 2) + 1);
            whereToSpawn.y = 0;

            while (Physics.CheckBox(whereToSpawn, FoodCheck, waterRotation, 31, QueryTriggerInteraction.Collide))
            {
                whereToSpawn.x = UnityEngine.Random.Range(-BoundX / 2, (BoundX / 2) + 1);
                whereToSpawn.z = UnityEngine.Random.Range(-BoundZ / 2, (BoundZ / 2) + 1);
            }
            Instantiate(HerbivourRef, whereToSpawn, this.transform.rotation);
        }
    
            //    
            //    whereToSpawn.y = 0;
            //}
            

        for (int i = 0; i < CarnivourCount; i++)
        {
            whereToSpawn.x = UnityEngine.Random.Range(-BoundX / 2, (BoundX / 2) + 1);
            whereToSpawn.z = UnityEngine.Random.Range(-BoundZ / 2, (BoundZ / 2) + 1);
            whereToSpawn.y = 0;

            while (Physics.CheckBox(whereToSpawn, FoodCheck, waterRotation, 31, QueryTriggerInteraction.Collide))
            {
                whereToSpawn.x = UnityEngine.Random.Range(-BoundX / 2, (BoundX / 2) + 1);
                whereToSpawn.z = UnityEngine.Random.Range(-BoundZ / 2, (BoundZ / 2) + 1);
            }


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
            gameObject.GetComponent<FoodScript>().initialise(BoundX, BoundZ, MaxFoodCount, MaxFoodDelay, MinFoodDelay, FoodRef);
        }



        int rnd = Random.Range(MinWaterDelay, MaxWaterDelay + 1);
        Invoke("ConstWaterSpawn", rnd);
    }

    private void ConstWaterSpawn()
    {
        UnityEngine.Vector3 whereToSpawn = transform.position;
        if (WaterList.Count < MaxWaterCount)
        {
            whereToSpawn.x = UnityEngine.Random.Range(-BoundX, BoundX + 1);
            whereToSpawn.z = UnityEngine.Random.Range(-BoundZ, BoundZ + 1);


            whereToSpawn.y = 0;
            Instantiate(WaterRef, whereToSpawn, this.transform.rotation);

            WaterList.Clear();
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Water"))
            {
                WaterList.Add(gameObject);
            }
        }


    }
}
