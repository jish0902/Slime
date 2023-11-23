using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObs : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.CompareTag("WaterAttack"))
        {
            Destroy(gameObject);
        }
    }
}
