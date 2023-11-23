using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Boolean is_dead;
    public int cur_health;
    public int max_health = 3;
    public float speed = 10;
    public float turnspeed = 20.0f;

    public GameObject water;
    public GameObject fire;
    public GameObject spawn;



    Vector3 movement;
    Quaternion rotation = Quaternion.identity;
    Rigidbody rb;

    Vector3 moveVector;

    void Start()
    {
        is_dead = false;
        rb = GetComponent<Rigidbody>();
        cur_health = max_health;
    }

    void Update()
    {
        
        if (is_dead)
        {
            Debug.Log("dead");
            transform.position = spawn.transform.position;
            is_dead = false;
        }
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement.Set(horizontal, 0f, vertical);
        movement.Normalize();

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnspeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);

        transform.rotation = rotation;
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Z))
        {

            Instantiate(water, transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.X ))
        {

            Instantiate(fire, transform.position, transform.rotation);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.rigidbody.velocity = Vector3.zero;
            collision.rigidbody.angularVelocity = Vector3.zero;
            rb.freezeRotation = true;
        }
    }
}