using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public int curr_spawn_slime_num;
    public int max_spawn_slime_num = 3;
    public GameObject spawn_slime;
    float timer = 0;
    
    void Awake()
    {
        curr_spawn_slime_num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (curr_spawn_slime_num < max_spawn_slime_num && timer > 3)
        {
            curr_spawn_slime_num++;
            Instantiate(spawn_slime,transform.position,transform.rotation);
            timer = 0;
        }
    }
}
