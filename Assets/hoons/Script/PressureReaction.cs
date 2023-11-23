using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureReaction : MonoBehaviour
{
    public Transform pressure_plate;
    private Renderer renderer;
    Boolean switch_on;
    // Start is called before the first frame update
    void Awake()
    {
        switch_on = pressure_plate.GetComponentInChildren<PressurePlate>().switch_on;
     
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch_on = pressure_plate.GetComponentInChildren<PressurePlate>().switch_on;

        if (switch_on)
        {
            renderer.material.color = Color.grey;

        }
        if (!switch_on)
        {
            renderer.material.color = Color.white;

        }
    }
}
