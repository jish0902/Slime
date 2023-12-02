using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Projectile : MonoBehaviour
{

    public float speed = 5f;
    public float expiryTime = 0f;
    public LivingEntity owner;
    public Vector3 Dir { get; set; }
    
    void Start ()
    {
        DestroyInTime(10f);
    }

    private void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
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
