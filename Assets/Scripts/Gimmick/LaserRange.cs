using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRange : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Smoke"))
        {
            transform.GetComponent<Collider>().isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Smoke"))
        {
            transform.GetComponent<Collider>().isTrigger = false ;
        }
    }
}
