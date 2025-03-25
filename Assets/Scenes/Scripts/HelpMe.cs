using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 m_Position;
    public float CameraSpeed;
    public float ScrollSpeed;

    void Start()
    {
        m_Position = this.transform.position;
        float BoundX = FindObjectOfType(typeof(Camera)).GetComponent<SimulationStarter>().BoundX;
        float BoundZ = FindObjectOfType(typeof(Camera)).GetComponent<SimulationStarter>().BoundZ;

        m_Position.y = MathF.Sqrt((BoundX * BoundX)+(BoundZ*BoundZ)) * 4;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
           
            m_Position.z += CameraSpeed / 50;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_Position.z -= CameraSpeed / 50;  
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_Position.x -= CameraSpeed / 50;
        }
        if(Input.GetKey(KeyCode.D))
        {
            m_Position.x += CameraSpeed / 50;
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            m_Position.y -= ScrollSpeed / 10;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            m_Position.y += ScrollSpeed / 10;
        }
        


        this.transform.position = m_Position;
    }
}
