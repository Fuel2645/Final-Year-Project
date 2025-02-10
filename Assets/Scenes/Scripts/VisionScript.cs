using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ownerRef;
    public List<GameObject> targets = new List<GameObject>();


    void OnCollisionEnter(Collision collision)
    {
        ownerRef.GetComponent<herbivorStuff>().AddEntity(collision.gameObject);
        targets.Add(collision.gameObject);
        print("Enter");
    }

    void OnCollisionExit(Collision collision)
    {
        ownerRef.GetComponent<herbivorStuff>().RemoveEntity(collision.gameObject);
        targets.Remove(collision.gameObject);
        print("Exit");
    }

}
