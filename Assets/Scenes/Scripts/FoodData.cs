using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodData : MonoBehaviour
{
    public float FoodValue = 40;

    public float EatFood()
    {
        Destroy(this.gameObject);
        return FoodValue;
       
    }


    FoodData(float foodValue)
    {
        FoodValue = foodValue;
    }
}
