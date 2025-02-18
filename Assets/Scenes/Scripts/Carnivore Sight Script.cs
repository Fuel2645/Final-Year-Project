using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivoreSightScript : MonoBehaviour
{
    public GameObject ownerRef;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        ownerRef.GetComponent<CarnivoreScript>().SpottedEntity(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        ownerRef.GetComponent<CarnivoreScript>().ForgetEntity(other.gameObject);
    }
}
