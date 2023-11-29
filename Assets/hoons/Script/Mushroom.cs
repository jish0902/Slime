using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    float limit_time = 3;
    float growth_time;
    Vector3 add_size = new Vector3(4, 4, 4);
    Vector3 maximum_size;
    Vector3 minimum_size;
    float duration = 2.0f;
    public Boolean is_growth;


    // Start is called before the first frame update
    void Awake()
    {
        is_growth = false;
        maximum_size = transform.localScale + add_size;
        minimum_size = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        growth_time += Time.deltaTime;
            
            if(growth_time > limit_time )
            {
                
                if(transform.localScale != minimum_size )
                {
                    transform.localScale -= new Vector3(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    is_growth = false;
                    growth_time = 0;
                }
                
            
        }
        if(Input.GetKeyDown(KeyCode.T)) {
            if(transform.localScale != maximum_size)
            {
                growth_time = 0;
                transform.localScale += new Vector3(0.4f, 0.4f, 0.4f);
            }
        }
    }
}
