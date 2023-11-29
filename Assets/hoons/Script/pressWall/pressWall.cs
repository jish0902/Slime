
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressWall : MonoBehaviour
{
    Transform leftWall;
    Transform rightWall;
    BoxCollider trap_collider;
    float move_speed = 1.2f;
    float trap_width;
    float move_distance;
    Boolean is_detected;
    Boolean is_moving;
    RightWall right;
    LeftWall left;
    Vector3 right_start;
    Vector3 left_start;
    Vector3 right_target;
    Vector3 left_target;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        is_detected = false;
        is_moving = false;
        rightWall = transform.GetChild(0);
        leftWall = transform.GetChild(1);
        trap_collider = GetComponent<BoxCollider>();
        trap_width = trap_collider.size.x;
        move_distance = trap_width / 2;
        right_start = rightWall.position;
        left_start = leftWall.position;
        right_target = rightWall.position + new Vector3(-move_distance, 0f, 0f);
        left_target = leftWall.position + new Vector3(move_distance, 0f, 0f);
        right = GetComponentInChildren<RightWall>();
        left = GetComponentInChildren<LeftWall>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (is_detected)
        {
            Debug.Log("detected");
            if (right.col_right) { Debug.Log("col_r"); }
            if (left.col_left) {Debug.Log("col_l"); }
            if (left.col_left && right.col_right)
            {
                player.GetComponent<Player>().is_dead = true;
                
            }

        }

        if (is_moving)
        {
            Vector3 right_cur_position = rightWall.position;
            Vector3 left_cur_position = leftWall.position;
            right_cur_position.x = Mathf.Lerp(right_cur_position.x, right_target.x, move_speed * Time.deltaTime);
            rightWall.position = right_cur_position;
            

            left_cur_position.x = Mathf.Lerp(left_cur_position.x, left_target.x, move_speed * Time.deltaTime);
            leftWall.position = left_cur_position;

            if (Mathf.Approximately(right_cur_position.x, right_target.x)||
                Mathf.Approximately(left_cur_position.x, left_target.x))
            {
                is_moving = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            right_target = rightWall.position + new Vector3(-move_distance, 0f, 0f);
            left_target = leftWall.position + new Vector3(move_distance, 0f, 0f);
            is_detected = true;
            is_moving = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            right_target = right_start;
            left_target = left_start;
            is_detected = false;
            is_moving = true;
        }

    }
}
