using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public class CarnivoreScript : MonoBehaviour
{

    public AIStates m_State;
    public CharacterController characterController;
    public GameObject ChasingEntity;
    public Vector3 moveDirection, TargetLocation, m_MovementVector;
    private float m_Speed;
    public float HuntChance;
    private float FoodCount = 100;
    public float DesireToHunt;
    public float ReductionRate = 2.5f;
    int NeedToHunt;
    float BoundX1 = 30, BoundX2 = -30, BoundZ1 = 30, BoundZ2 = -30;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        m_MovementVector = this.transform.position;
        TargetLocation = this.transform.position;
        m_Speed = 10.0f;

        NeedToHunt = UnityEngine.Random.Range(50, 71);

        InvokeRepeating("StateCheck", 1.0f, 0.5f);
        InvokeRepeating("Phyiscs", 0.0f, 0.05f);
        InvokeRepeating("FoodDrain", 1.0f, 0.5f);
    }



    // Update is called once per frame
    void Update()
    {
    }

    void Phyiscs()
    {
        if (TargetLocation.x >= BoundX1)
        {
            TargetLocation.x = BoundX1;
        }
        else if (TargetLocation.x <= BoundX2)
        {
            TargetLocation.x = BoundX2;
        }

        if (TargetLocation.z >= BoundZ1)
        {
            TargetLocation.z = BoundZ1;
        }
        else if (TargetLocation.z <= BoundZ2)
        {
            TargetLocation.z = BoundZ2;
        }

        moveDirection = TargetLocation - this.transform.position;
        m_MovementVector = moveDirection.normalized * m_Speed * 0.05f;
        if (moveDirection.magnitude < 0.5)
        {
            this.transform.position = TargetLocation;
            //print("Stuck");
        }
        else
        {
            //print("Moving");

            this.transform.LookAt(TargetLocation);
            characterController.Move(m_MovementVector);
        }
    }
    void StateCheck()
    {
        HuntChance = 0 + FoodCount - DesireToHunt;  
        //

        if(m_State == AIStates.Idle)
        {
            if (HuntChance > NeedToHunt)
            {
                m_State = AIStates.Idle;
            }
            else if (HuntChance <= NeedToHunt)
            {
                m_State = AIStates.Finding_Food;
            }
        }
        
       



        StateMachine();
    }



    void StateMachine()
    {
        switch (m_State)
        {
            case AIStates.Idle:
                int rnd = Random.Range(0, 2);
                print("Carnviore Move");
                if (rnd == 0)
                {
                    TargetLocation.x += Random.Range(-10, 11);
                    TargetLocation.z += Random.Range(-10, 11);
                }
                else
                {
                    TargetLocation = transform.position;
                }
                break;
            case AIStates.Fleeing:
                break;
            case AIStates.Finding_Water:
                break;
            case AIStates.Finding_Food:
                if(ChasingEntity == null)
                {
                    TargetLocation.x += Random.Range(-10, 11);
                    TargetLocation.z += Random.Range(-10, 11);
                }
                break;
            case AIStates.Eating:
                FoodCount += 25;
                ChasingEntity.GetComponent<herbivorStuff>().GetEatenBitch();
                NeedToHunt = Random.Range(50, 71);
                break;
            case AIStates.Drinking:
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Herbivore") && other is not BoxCollider)
        {
            print("Herbivore");
            if (ChasingEntity == null)
            {
                ChasingEntity = other.gameObject;
            }
            else if (Vector3.Distance(this.transform.position, other.gameObject.transform.position) < Vector3.Distance(this.transform.position, ChasingEntity.transform.position)) 
            {
                ChasingEntity = other.gameObject;
            }
        }
    }


    void FoodDrain()
    {
        FoodCount += ReductionRate;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ChasingEntity)
        {
            ChasingEntity = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == ChasingEntity)
        {
            m_State = AIStates.Eating;
        }
    }
}
