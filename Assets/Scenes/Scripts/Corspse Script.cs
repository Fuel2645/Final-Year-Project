using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorspseScript : MonoBehaviour
{
    public int Health = 50;

    public void EatMe()
    {
        Health -= 25;
        DeathCheck();
    }

    void DeathCheck()
    {
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
