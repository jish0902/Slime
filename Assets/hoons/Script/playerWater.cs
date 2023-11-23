using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWater : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward * 0.1f);
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (other.transform.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }


}
