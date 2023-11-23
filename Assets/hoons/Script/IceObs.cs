using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceObs : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.CompareTag("FireAttack"))
        {
            Destroy(gameObject);
        }
    }
}
