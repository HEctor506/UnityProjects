using System;
using UnityEngine;

public class SceenBounds : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.4f, 2.4f), transform.position.y, transform.position.z);
        
    }
}
