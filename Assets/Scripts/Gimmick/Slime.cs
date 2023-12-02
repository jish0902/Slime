using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour
{
    public GameObject player;
    public float slimeDistance = 2.0f;
    public float maxDistance = 5.0f;
    public Vector3 lastDir =- Vector3.back;

    public Define.SlimeState state = Define.SlimeState.None;
    
    private Rigidbody rb;
    private NavMeshAgent agent;
    private bool isMode = false;
    private PlayerCharacter pc;


    [Header("InGame")] 
    public int WaterSpeed = 10;
    public int FireSpeed = 10;
    public int SoilSpeed = 10;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        pc = player.GetComponent<PlayerCharacter>();
    }
    void Update()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        if(!isMode)
            Following(player.transform.position);
        
    }


    public void ChangeState(Define.SlimeState _state)
    {
        //이펙트
        //슬라임 색 변환
        state = _state;
    }
    
    public void Fire()
    {
        string target = $"Projectile/{state.ToString()}Ball";
        Debug.Log(target);
        GameObject go = Managers.Resource.Instantiate(target);
        Projectile pj = go.GetComponent<Projectile>();
        pj.owner = pc;
        
        switch (state)
        {
            case Define.SlimeState.Water:
                go.transform.position = transform.position;
                go.transform.rotation = transform.rotation;
                break;
            
            case Define.SlimeState.Fire:
                break;
            
            case Define.SlimeState.Soil:
                float distance = 10f;
                Quaternion toRotate = Quaternion.LookRotation(transform.forward);
                pj.GetComponent<Rigidbody>().velocity = transform.up * (10f * SoilSpeed);
                break;
            
            case Define.SlimeState.Smoke:
                break;
            case Define.SlimeState.Metal:
                break;
            case Define.SlimeState.Tree:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        

    }
    

    public void HandGunMode(Transform tr, bool _isMode)
    {
        if(isMode == true && _isMode == true)
            return;
        
        if(isMode == false && _isMode == false)
            return;
        
        isMode = _isMode;
        if (_isMode)
        {
            AgentPause();
            agent.enabled = false;
            rb.isKinematic = true;

            transform.position = tr.position;
            transform.parent = tr;
        }
        else
        {
            rb.isKinematic = false;
            agent.enabled = true;
            transform.position = transform.position = player.transform.position + lastDir;
            transform.parent = null;
            AgentResume();
        }
    }
    
    void Following(Vector3 target)
    {
        float slimeToPlayerDistance = Vector3.Distance(target, transform.position);

        if (slimeToPlayerDistance > maxDistance)
        {
            Vector3 temp = target - transform.position;
            lastDir = -Vector3.Normalize(new Vector3(temp.x,0,temp.z));
            transform.position = target + lastDir;
        }

        if(slimeToPlayerDistance < slimeDistance)
        {
            AgentPause();
        }
        else
        {
            AgentResume();
        }
        
        
    }


    private void AgentPause()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    private void AgentResume()
    {
        agent.isStopped= false;
        agent?.SetDestination(player.transform.position);
    }


}
