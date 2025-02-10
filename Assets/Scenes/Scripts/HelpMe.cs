using System;
using System.Collections;
using System.Collections.Generic;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
           
            m_Position.y += CameraSpeed / 50;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_Position.y -= CameraSpeed / 50;  
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_Position.x -= CameraSpeed / 50;
        }
        if(Input.GetKey(KeyCode.A))
        {
            m_Position.x += CameraSpeed / 50;
        }
        if(Input.mouseScrollDelta.y <0 )
        {
            m_Position.z -= CameraSpeed / 10;
        }
        else if(Input.mouseScrollDelta.y > 0 )
        {
            m_Position.z += CameraSpeed / 10;
        }
        if(Input.GetAxis("Mouse Y") != 0 && Input.GetAxis("Mouse X") != 0)
        {
            
        }

        this.transform.position = m_Position;
    }
}
