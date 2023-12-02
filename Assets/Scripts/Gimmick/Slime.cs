using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour
{
    public Transform player;
    public float slimeDistance = 2.0f;
    public float maxDistance = 5.0f;
    public Vector3 lastDir =- Vector3.back;

    public Define.SlimeState state;
    
    private Rigidbody rb;
    private NavMeshAgent agent;
    private bool isMode = false;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
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
<<<<<<< HEAD
        string target = $"Projectile/{state.ToString()}Ball";
        Debug.Log(target);
        GameObject go = Managers.Resource.Instantiate(target);
        Projectile pj = go.GetComponent<Projectile>();
        pj.owner = pc;
        pj.Dir = pc.Dir;
        
        switch (state)
        {
            case Define.SlimeState.Water:
                go.transform.position = transform.position;
                go.transform.rotation = transform.rotation;
                break;
            
            case Define.SlimeState.Fire:
                go.transform.position = transform.position;
                go.transform.rotation = transform.rotation;
                break;
            
            case Define.SlimeState.Soil:
                float distance = 10f;
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
        
        
=======

        GameObject go = Managers.Resource.Instantiate($"Projectile/{state.ToString()}");
         go.GetComponent<Projectile>();
>>>>>>> parent of 7d17af6 (Bullet 재구성)

    }
    

    public void HandGunMode(Transform tr, bool _isMode)
    {
        if(isMode == true && _isMode == true)
            return;
        
        isMode = _isMode;
        if (_isMode)
        {
            AgentPause();
            rb.useGravity = false;
            agent.baseOffset = 1f;

            transform.position = tr.position;
            transform.parent = tr;
        }
        else
        {
            AgentResume();
            rb.useGravity = true;
            agent.baseOffset = 0.1f;
            transform.position = transform.position = player.transform.position + lastDir;
            transform.parent = null;
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
        agent?.SetDestination(player.position);
    }


}
