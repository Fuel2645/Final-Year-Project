using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbivoreVisionScript : MonoBehaviour
{
    private GameObject parentOBJ;
    private void OnEnable()
    {
        parentOBJ = this.transform.parent.gameObject;

    }
    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag == ("Food") && !parentOBJ.GetComponent<herbivorStuff>().FoundFood.Contains(other.gameObject))
        {
            parentOBJ.GetComponent<herbivorStuff>().FoundFood.Add(other.gameObject);

        }
        else if (other.gameObject.tag == ("Water") && !parentOBJ.GetComponent<herbivorStuff>().FoundWater.Contains(other.gameObject))
        {

            parentOBJ.GetComponent<herbivorStuff>().FoundWater.Add(other.gameObject);
        }
        else if (other.gameObject.tag == ("Carnivore") && other is not BoxCollider)
        {
            print("Carnivore");
            parentOBJ.GetComponent<herbivorStuff>().m_State = AIStates.Fleeing;
            parentOBJ.GetComponent<herbivorStuff>().PreviousTargetLocation = parentOBJ.GetComponent<herbivorStuff>().TargetLocation;
            parentOBJ.GetComponent<herbivorStuff>().TargetLocation += (this.transform.position - other.transform.position).normalized * 10;
            parentOBJ.GetComponent<herbivorStuff>().chasingCarnivore = other.gameObject;
            parentOBJ.GetComponent<herbivorStuff>().isMoving = false;


        }
        else
        {
            // print("dick");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Food") && parentOBJ.GetComponent<herbivorStuff>().FoundFood.Contains(other.gameObject))
        {
            parentOBJ.GetComponent<herbivorStuff>().FoundFood.Remove(other.gameObject);
        }
    }
}
