using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivoreSightScript : MonoBehaviour
{
    private GameObject parentOBJ;
    private void OnEnable()
    {
        parentOBJ = this.transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Herbivore") && other is not BoxCollider)
        {
            print("Herbivore");
            if (parentOBJ.GetComponent<CarnivoreScript>().ChasingEntity == null)
            {
                parentOBJ.GetComponent<CarnivoreScript>().ChasingEntity = other.gameObject;
            }
            //else if (Vector3.Distance(parentOBJ.transform.position, other.gameObject.transform.parent.position) < Vector3.Distance(parentOBJ.transform.position, parentOBJ.GetComponent<CarnivoreScript>().ChasingEntity.transform.position))
            //{
            //    parentOBJ.GetComponent<CarnivoreScript>().ChasingEntity = other.gameObject;
            //}
        }
        else if (other.gameObject.tag == ("Corpse") && other.gameObject != parentOBJ.GetComponent<CarnivoreScript>().ChasingEntity)
        {
            print("Corpse");
            if (parentOBJ.GetComponent<CarnivoreScript>().ChasingEntity == null)
            {
                parentOBJ.GetComponent<CarnivoreScript>().ChasingEntity = other.gameObject;
            }
            //else if (Vector3.Distance(parentOBJ.transform.position, other.gameObject.transform.position) < Vector3.Distance(parentOBJ.transform.position, parentOBJ.GetComponent<CarnivoreScript>().ChasingEntity.transform.position))
            //{
            //    parentOBJ.GetComponent<CarnivoreScript>().ChasingEntity = other.gameObject;
            //}
        }
        else if (other.gameObject.tag == ("Water") && !parentOBJ.GetComponent<CarnivoreScript>().FoundWater.Contains(other.gameObject))
        {
            parentOBJ.GetComponent<CarnivoreScript>().FoundWater.Add(other.gameObject);
        }
    }

}
