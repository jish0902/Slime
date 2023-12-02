using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Projectile : MonoBehaviour {

    public float expiryTime = 0f;
    public LivingEntity owner;
    public Vector3 Dir { get; protected set; }
    
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

    public virtual void Init()
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
