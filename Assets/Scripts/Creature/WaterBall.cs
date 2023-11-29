using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward * 0.01f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other. tag);
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerCharacter>().player_HP -= 1;
            Destroy(gameObject);
        }
        
    }
}
