using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 5f;
    public float expiryTime = 0f;
    public LivingEntity owner;
<<<<<<< HEAD
    public Vector3 Dir { get; set; }
=======

>>>>>>> parent of 7d17af6 (Bullet 재구성)
    
    void Start ()
    {
        DestroyInTime(10f);
    }
<<<<<<< HEAD

    private void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
    }
=======
    
>>>>>>> parent of 7d17af6 (Bullet 재구성)
    
    public void DestroyInTime(float time)
    {
        Invoke(nameof(_t) ,time);
    }

    private void _t()
    {
        Managers.Resource.Destroy(gameObject); 
    }
    
}
