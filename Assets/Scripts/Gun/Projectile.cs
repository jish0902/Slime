using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float expiryTime = 0f;
    public LivingEntity owner;

    
    void Start ()
    {
        DestroyInTime(10f);
    }
    
    
    public void DestroyInTime(float time)
    {
        Invoke(nameof(_t) ,time);
    }

    private void _t()
    {
        Managers.Resource.Destroy(gameObject); 
    }
    
}
