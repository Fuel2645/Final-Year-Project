using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;




public enum AIStates
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
    private CharacterController characterController;
    private Vector3 FoodLocaiton, moveDirection;
    public AIStates m_State;
    private float FoodCount = 100;
    public float WaterCount = 100;
    private float m_Speed;
    private int m_Health;
    private Vector3 TargetLocation;
    private Vector3 m_MovementVector;
    private bool isMoving = false;
    private bool isEating;
    private bool isDrinking;
    private float ReductionRate;
    private float WaterReductionRate;
    private GameObject chasingCarnivore;
    private List<GameObject> FoundFood;
    public List<GameObject> FoundWater;
    private SphereCollider sphereCollider;
    private GameObject CorpseRef;
    private float BoundX1, BoundX2, BoundZ1, BoundZ2;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        m_MovementVector = this.transform.position;
        TargetLocation = this.transform.position;
        //FoodLocaiton = FoodReference.transform.position;
        
        FoundFood = new List<GameObject>(); 
        FoundWater = new List<GameObject>();
       UnityEngine.Physics.IgnoreCollision(this.GetComponent<SphereCollider>(), this.GetComponent<SphereCollider>(), true);
        initialise(20,20,20,20,this.gameObject);
    }

    // Update is called once per frame
    public void initialise(float BoundX, float BoundZ, int Health, float Speed, GameObject Corpse)
    {
        ReductionRate = -2.5f;
        WaterReductionRate = -20.0f;
        CorpseRef = Corpse;

        print("Herbivore Start");

        BoundX1 = BoundX;
        BoundX2 = -1 * BoundX;
        BoundZ1 = BoundZ;
        BoundZ2 = -1 * BoundZ;

        m_Health = Health;

        m_Speed = Speed;

        m_State = AIStates.Idle;
        InvokeRepeating("StateCheck", 1.0f, 0.5f);
        InvokeRepeating("Physics", 1.0f, 0.05f);
        //InvokeRepeating("FoodDrain", 0, 0.5f);
        InvokeRepeating("WaterDrain",0,1.0f);
    }

    void StateCheck()
    {
        if(FoundFood.Count > 1)
        {
            FoundFood.RemoveAll(item => item == null);
        }
        else if(FoundFood.Count > 0)
        {
            if(FoundFood.FirstOrDefault() == null)
            {
                FoundFood.Remove(FoundFood.FirstOrDefault());   
            }
        }
        
        if (m_State != AIStates.Fleeing)
        {   
            if (FoodCount > 50 && WaterCount > 60)
            {
                m_State = AIStates.Idle;
            }
            else if(WaterCount <= 60 && isMoving == false)
            {
                m_State = AIStates.Finding_Water;
            }
            else if (FoodCount <= 50 && isMoving == false)
            {
                print("Help");
                m_State = AIStates.Finding_Food;
            }
            else if(this.transform.position == TargetLocation && isDrinking == true)
            {
                m_State = AIStates.Drinking;
            }
            else if (this.transform.position == TargetLocation && isEating == true)
            {
                m_State = AIStates.Eating;
            }
            Statemachine();
        }
        else
        {
            if (chasingCarnivore != null && Vector3.Distance(chasingCarnivore.transform.position, this.transform.position) >= 10)
            {
                m_State = AIStates.Idle;
            }
            else if (chasingCarnivore != null && this.transform.position == TargetLocation)
            {
                TargetLocation += (this.transform.position - chasingCarnivore.transform.position).normalized * 10;
                print("LocationInfo");
            }
            else if (chasingCarnivore == null)
            {
                m_State = AIStates.Idle;
            }
        }
    }

    void FoodDrain()
    {
        
        FoodCount = Mathf.Clamp(FoodCount + ReductionRate, 0, 100);
        if(FoodCount == 0)
        {
            m_Health -= 5;
            DeathCheck();
        }

    }

    void WaterDrain()
    {
        print("Drain");
        WaterCount = Mathf.Clamp(WaterCount + WaterReductionRate, 0, 100);
        if(WaterCount == 0)
        {
            m_Health -= 5;
            DeathCheck();
        }
    }


    void Statemachine()
    {
        switch (m_State)
        {
            case AIStates.Idle:
                int rnd = Random.Range(0, 2);
                if (rnd == 0)
                {
                    TargetLocation.x += Random.Range(-10, 11);
                    TargetLocation.z += Random.Range(-10, 11);
                    TargetLocation.y = 0;
                }
                else
                {
                    TargetLocation = this.transform.position;
                }

                if (FoodCount >= 70)
                {
                    m_Health += 5;
                }
                break;
            case AIStates.Fleeing:
                break;
            case AIStates.Finding_Food:
                if (FoundFood.Count == 0)
                {
                    TargetLocation.x += Random.Range(-20, 10);
                    TargetLocation.z += Random.Range(-20, 10);
                }
                else if (isMoving == true)
                {
                    if (TargetLocation == this.transform.position)
                    {
                        isEating = true;
                        isMoving = false;
                    }
                }
                else if (isMoving == false)
                {
                    // sorts Foods based on distance from the player in ascending order, closest == first 
                    if (FoundFood.Count > 1)
                    {
                        print("Sorting");
                        FoundFood.Sort((x, y) => { return (this.transform.position - x.transform.position).sqrMagnitude.CompareTo((this.transform.position - y.transform.position).sqrMagnitude); });
                        TargetLocation = FoundFood.FirstOrDefault().transform.position;
                        isMoving = true;
                    }
                    else if (FoundFood.Count > 0)
                    {
                        TargetLocation = FoundFood.FirstOrDefault().transform.position;
                        isMoving = true;
                    }
                }
                break;
            case AIStates.Finding_Water:
                if (FoundWater.Count == 0)
                {
                    TargetLocation.x += Random.Range(-20, 10);
                    TargetLocation.z += Random.Range(-20, 10);
                }
                else if (isMoving == true)
                {
                    if (TargetLocation == this.transform.position)
                    {
                        isDrinking = true;
                        isMoving = false;
                    }
                }
                break;
            case AIStates.Drinking:
                isDrinking = false;
                isMoving = false;
                WaterCount += FoundWater.FirstOrDefault().GetComponent<WaterScript>().DrinkingWater();
                break;
            case AIStates.Eating:
                isEating = false;
                isMoving = false;
                FoodCount += FoundFood.First().GetComponent<FoodData>().EatFood();
                break;
                
        }
    }

    void SightClear()
    {
        for (int i = FoundFood.Count; i < 0; i--)
        {
            if (FoundFood[i] == null)
            {
                FoundFood.RemoveAt(i);
            }
        }
    }

    public void GetEatenBitch()
    {
        m_State = AIStates.Fleeing;
        print("I got bit");
        m_Health -= 25;
        DeathCheck();
    }

    void DeathCheck()
    {
        if (m_Health <= 0)
        {
            Instantiate(CorpseRef,this.transform.position, this.transform.rotation);
            this.tag = CorpseRef.tag;
            Destroy(this.gameObject);
            
        }

    }

    void Physics()
    {
        if(TargetLocation.x >= BoundX1 )
        {
            TargetLocation.x = BoundX1;
        }
        else if(TargetLocation.x <= BoundX2)
        {
            TargetLocation.x = BoundX2;
        }

        if(TargetLocation.z >= BoundZ1 )
        {
            TargetLocation.z = BoundZ1;
        }
        else if( TargetLocation.z <= BoundZ2)
        {
            TargetLocation.z = BoundZ2;
        }


        moveDirection = TargetLocation - this.transform.position;
        m_MovementVector = moveDirection.normalized * m_Speed * 0.05f;
        if (moveDirection.magnitude < 0.5)
        {
            this.transform.position = TargetLocation;
        }
        else
        {

            this.transform.LookAt(TargetLocation);
            //this.transform.position = this.transform.position + m_MovementVector;
            characterController.Move(m_MovementVector);
           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == sphereCollider)
        {
            print("Testing");
            // Carnviore attack idk
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Food") && !FoundFood.Contains(other.gameObject))
        {
            print("balls");
            if (FoundFood.Count == 0 && m_State != AIStates.Fleeing)
            {
                TargetLocation = other.transform.position;
            }


            FoundFood.Add(other.gameObject);

        }
        else if (other.gameObject.tag == ("Water") && !FoundWater.Contains(other.gameObject))
        {
            print("Water");
            if(FoundWater.Count == 0 && m_State != AIStates.Fleeing)
            {
                TargetLocation = other.transform.position;
            }

            FoundWater.Add(other.gameObject);
        }
        else if (other.gameObject.tag == ("Carnivore") && other is not BoxCollider)
        {
            print("Carnivore");
            m_State = AIStates.Fleeing;
            TargetLocation += (this.transform.position - other.transform.position).normalized * 10;
            chasingCarnivore = other.gameObject;
        }
        else print("dick");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Food") && FoundFood.Contains(other.gameObject))
        {
            FoundFood.Remove(other.gameObject);
        }
    }
}
