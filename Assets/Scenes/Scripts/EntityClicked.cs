using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityClicked : MonoBehaviour
{
    public void OnMouseDown()
    {
        print("You clicked me " + this.gameObject.name);
    }
}
