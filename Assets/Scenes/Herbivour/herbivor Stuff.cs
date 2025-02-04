using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class herbivorStuff : MonoBehaviour
{
    public GameObject FoodReference;
    public SphereCollider SphereCollider;
    public CharacterController CharacterController;

    // Start is called before the first frame update
    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        Vector3 FoodLocaiton = FoodReference.transform.position;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x + 0.1f, 0, 0);
    }
}
