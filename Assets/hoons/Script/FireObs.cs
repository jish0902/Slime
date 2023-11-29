using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObs : Gimmick
{
    public string Target = "WaterAttack";
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.CompareTag(Target))
        {
            Destroy(gameObject);
        }
    }
}
