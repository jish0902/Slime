using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Transform switched_object; //switched object의 스크립트에 온오프 기능 넣기
    Transform plate;
    public Boolean switch_on;
    Boolean isMoving;
    float initYposition;
    float targetYPosition;

    // Start is called before the first frame update
    void Awake()
    {
        switch_on = false;
        plate = transform.parent;

        initYposition = plate.transform.position.y;
        targetYPosition = initYposition;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isMoving)
        {
           
            Vector3 currentPositon = plate.transform.position;

            currentPositon.y = Mathf.Lerp(currentPositon.y, targetYPosition, 2 * Time.deltaTime);
            plate.transform.position = currentPositon;

            if (Mathf.Approximately(currentPositon.y, targetYPosition)){
                isMoving = false;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){ //player -> metal Slime
            targetYPosition = initYposition - 0.1f;
            switch_on = true;
            isMoving = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            targetYPosition = initYposition;
            switch_on = false;
            isMoving = true;
        }
    }
}
