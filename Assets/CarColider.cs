﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {

        Debug.Log("Car Hit " + other.transform.name);
        if (other.tag == "Car")
        {
            Debug.Log("Car Hit " + other.transform.name);
            gameManager.getPlayer(other.transform.name).RpcAddDamge();
        }
    }
    
}
