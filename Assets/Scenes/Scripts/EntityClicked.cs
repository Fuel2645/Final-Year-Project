using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityClicked : MonoBehaviour
{
    public void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Fps>().selectedObject = this.gameObject;
    }
}
