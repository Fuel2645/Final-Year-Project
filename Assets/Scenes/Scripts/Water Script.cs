using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private float WaterCount = 10000;

    public float DrinkingWater()
    {
        if (WaterCount - 40 <= 0)
        {
            Destroy(this.gameObject);
        }
        return WaterCount -= 40;
        
    }
}
