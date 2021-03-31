using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 cameraDistance; 

    void Start()
    {
        cameraDistance = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + cameraDistance;
    }
}
