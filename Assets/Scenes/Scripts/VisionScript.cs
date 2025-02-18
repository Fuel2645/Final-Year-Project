using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ownerRef;


    private void OnTriggerEnter(Collider other)
    {
        print("I Discovered SOmmat");
        ownerRef.GetComponent<herbivorStuff>().addEntitiy(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        ownerRef.GetComponent<herbivorStuff>().removeEntity(other.gameObject);
    }

}
