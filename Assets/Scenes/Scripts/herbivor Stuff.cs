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
    public AIStates m_State;

    private float FoodCount = 100;
    private float WaterCount = 100;
    private float m_Speed;
    private float BoundX1, BoundX2, BoundZ1, BoundZ2;
    private float ReductionRate;
    public bool isMoving = false;
    private int m_Health;
    private Vector3 FoodLocaiton, moveDirection;
    private Vector3 TargetLocation;
    private Vector3 m_MovementVector;
    private Vector3 ReachBox;
    private Vector3 PreviousTargetLocation;
    private GameObject chasingCarnivore;
    public List<GameObject> FoundFood;
    public List<GameObject> FoundWater;
    private SphereCollider sphereCollider;
    private GameObject CorpseRef;
    private CharacterController characterController;
    public float distgancse;
    



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
        ReachBox = new Vector3(1.5f, 3.0f, 1.5f);
    }

    // Update is called once per frame
    public void initialise(float BoundX, float BoundZ, int Health, float Speed, GameObject Corpse)
    {
        ReductionRate = -2.5f;
        CorpseRef = Corpse;
        float NextBirthingTime = Random.Range(20.0f, 40.0f);

        print("Herbivore Start");

        BoundX1 = BoundX;
        BoundX2 = -1 * BoundX;
        BoundZ1 = BoundZ;
        BoundZ2 = -1 * BoundZ;

        m_Health = Health;

        m_Speed = Speed;

        m_State = AIStates.Idle;

        float rnd1, rnd2;
        rnd1 = Random.Range(0.2f, 0.9f);
        rnd2 = Random.Range(0.7f, 1.3f);

        InvokeRepeating("StateCheck", rnd2, rnd1);
        InvokeRepeating("physics", 1.0f, 0.05f);
        InvokeRepeating("FoodDrain", 1.0f, 0.5f);
        InvokeRepeating("WaterDrain",1.0f,1.0f);
        Invoke("Birthing", NextBirthingTime);
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
        
        if (m_State == AIStates.Idle)
        {   
            if (FoodCount > 50 && WaterCount > 60)
            {
                m_State = AIStates.Idle;
            }
            else if(WaterCount <= 60)
            {
                m_State = AIStates.Finding_Water;
            }
            else if (FoodCount <= 50)
            {
                print("Help");
                m_State = AIStates.Finding_Food;
            }
        }
        else if(m_State == AIStates.Fleeing) 
        {
            if (chasingCarnivore != null && Vector3.Distance(chasingCarnivore.transform.position, this.transform.position) >= 10)
            {
                m_State = AIStates.Idle;
                TargetLocation = PreviousTargetLocation;
            }
            else if (chasingCarnivore != null && this.transform.position == TargetLocation)
            {
                TargetLocation += (this.transform.position - chasingCarnivore.transform.position).normalized * 10;
                print("LocationInfo");
            }
            else if (chasingCarnivore == null)
            {
                m_State = AIStates.Idle;
                TargetLocation = PreviousTargetLocation;
            }
        }
        Statemachine();
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
        WaterCount = Mathf.Clamp(WaterCount + ReductionRate, 0, 100);
        if(WaterCount == 0)
        {
            m_Health -= 5;
            DeathCheck();
        }
    }

    private void Birthing()
    {
        if (m_Health >= 25 && FoodCount >= 20 && WaterCount >= 20)
        {
            print("Giving Birth");
            GameObject child = new GameObject();
            float childSpeed = Random.Range(m_Speed - 0.2f, m_Speed + 0.3f);
            child = Instantiate(this.gameObject, this.transform.position, this.transform.rotation);
            child.GetComponent<herbivorStuff>().initialise(BoundX1, BoundZ1, 100, childSpeed, CorpseRef);
        }

        float NextBirthingTime = Random.Range(20.0f, 40.0f);
        Invoke("Birthing", NextBirthingTime);
    }
    void Statemachine()
    {
        switch (m_State)
        {
            case AIStates.Idle:
                int rnd = Random.Range(0, 2);
                if (rnd == 0)
                {
                    TargetLocation = this.transform.position;
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
                    TargetLocation = this.transform.position;
                    TargetLocation.y = 0;
                    TargetLocation.x += Random.Range(-20, 21);
                    TargetLocation.z += Random.Range(-20, 21);
                    print("Idle Move");
                }
                else if (isMoving == true)
                {
                    print("Is Moving");
                    if (Vector3.Distance(this.transform.position, FoundFood.First().transform.position) <= 2)
                    {
                        isMoving = false;
                        m_State = AIStates.Eating;
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
                        print("First or default");
                        TargetLocation = FoundFood.FirstOrDefault().transform.position;
                        isMoving = true;
                    }
                }
                break;
            case AIStates.Finding_Water:
                if (FoundWater.Count == 0)
                {
                    TargetLocation = this.transform.position;
                    TargetLocation.y = 0;
                    TargetLocation.x += Random.Range(-20, 21);
                    TargetLocation.z += Random.Range(-20, 21);
                    
                }
                else if (isMoving == true)
                {
                    foreach( Collider collider in Physics.OverlapBox(this.transform.position, ReachBox))
                    {
                        if(collider.gameObject.tag == "Water")
                        {
                            isMoving = false;
                            m_State = AIStates.Drinking;
                        }
                    }    
                    
                }
                else if (isMoving == false)
                {
                    if(FoundWater.Count() > 1)
                    {
                        FoundWater.Sort((x, y) => { return (this.transform.position - x.transform.position).sqrMagnitude.CompareTo((this.transform.position - y.transform.position).sqrMagnitude); });
                        TargetLocation = FoundWater.FirstOrDefault().transform.position;
                        isMoving = true;
                    }
                    else if(FoundWater.Count() > 0)
                    {

                        TargetLocation = FoundWater.FirstOrDefault().transform.position;
                        isMoving = true;
                    }

                }
                break;
            case AIStates.Drinking:
                FoundWater.First().GetComponent<WaterScript>().DrinkingWater();
                WaterCount += 40;
                m_State = AIStates.Idle;
                break;
            case AIStates.Eating:
                if(FoundFood.Count() > 0)
                {
                    FoundFood.First().GetComponent<FoodData>().EatFood();
                    FoodCount += 40;
                    m_State = AIStates.Idle;
                }
                else
                {
                    m_State = AIStates.Finding_Food;
                }
               
                
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


    public void TouchingWater()
    {

        if (m_State == AIStates.Finding_Water)
        {
            TargetLocation = this.transform.position;
            isMoving = false;
            m_State = AIStates.Drinking;
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

    void physics()
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
        if (Vector3.Distance(this.transform.position, TargetLocation) <= Vector3.Distance(this.transform.position + m_MovementVector, TargetLocation))
        {
            this.transform.position = TargetLocation;
            isMoving = false;
            //print("Stuck");
        }
        else
        {

            this.transform.LookAt(TargetLocation);
            //this.transform.position = this.transform.position + m_MovementVector;
            characterController.Move(m_MovementVector);
           
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider == sphereCollider)
    //    {
    //        print("Testing");
    //        // Carnviore attack idk
    //    }

    //}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Food") && !FoundFood.Contains(other.gameObject))
        {
            FoundFood.Add(other.gameObject);

        }
        else if (other.gameObject.tag == ("Water") && !FoundWater.Contains(other.gameObject))
        {

            FoundWater.Add(other.gameObject);
        }
        else if (other.gameObject.tag == ("Carnivore") && other is not BoxCollider)
        {
            print("Carnivore");
            m_State = AIStates.Fleeing;
            PreviousTargetLocation = TargetLocation;
            TargetLocation += (this.transform.position - other.transform.position).normalized * 10;
            chasingCarnivore = other.gameObject;
            isMoving = false;
            
          
        }
        else
        {
           // print("dick");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Food") && FoundFood.Contains(other.gameObject))
        {
            FoundFood.Remove(other.gameObject);
        }
    }
}
