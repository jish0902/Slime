using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FireBall : MonoBehaviour
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
        else if (other.transform.CompareTag("Torch"))
        {
            Destroy(gameObject);
            if (other.transform.GetComponent<Light>().intensity < 10)
            {
                other.transform.GetComponent<Light>().intensity += 5;
            }
            
        }
    }
}
