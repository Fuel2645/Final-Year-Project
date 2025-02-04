using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




enum AIStates
{
    Idle,
    Fleeing,
    Finding_Food,
    Eating,
    Finding_Water,
    Drinking
}


public class herbivorStuff : MonoBehaviour
{
    public GameObject Vision;
   
    public SphereCollider SphereCollider;
    public CharacterController CharacterController;
    public Vector3 FoodLocaiton, moveDirection;
    private AIStates m_State = AIStates.Idle;
    private int FoodCount;
    private float m_Speed;
    private Vector3 TargetLocation;
    private Vector3 m_MovementVector;

    // Start is called before the first frame update
    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        //FoodLocaiton = FoodReference.transform.position;
        m_Speed = 10.0f;
        InvokeRepeating("StateCheck", 1.0f, 0.5f);
        InvokeRepeating("Physics", 1.0f, 0.05f);

    }

    // Update is called once per frame

    void StateCheck()
    {
       

    }

    void Statemachine()
    {
        switch (m_State)
        {
            case AIStates.Idle:
                break;
            case AIStates.Fleeing:
                break;
            case AIStates.Finding_Food:
                break;
            case AIStates.Finding_Water:
                break;
            case AIStates.Drinking:
                break;
            case AIStates.Eating:
                break;
                
        }
    }

    void Physics()
    {
        moveDirection = TargetLocation - transform.position;
        m_MovementVector = moveDirection.normalized * m_Speed * 0.05f;
        if (moveDirection.magnitude < 0.1)
        {
            transform.position = TargetLocation;
        }
        else
        {
            transform.LookAt(FoodLocaiton);
            CharacterController.Move(m_MovementVector);
        }
    }
}
