using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterSlime : MonoBehaviour
{
    RaycastHit hit;
    NavMeshAgent agent;
    Animator animator;
    Rigidbody rb;
    GameObject player;
    public GameObject waterBall;
    public GameObject waterBallSpawn;
    public GameObject spawner;

    public int slime_hp;
    int slime_maxHp = 3;
    float distance_of_slime_to_player;
    float distance_of_slime_to_spawner;
    float attackRange = 15;
    float chasingRange = 20;
    float detectRange = 10;
    float limitRange = 20;
    float attackDelay = 5; 
    float time_after_attack = 0;
    float death_motion_time = 2;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        slime_hp = slime_maxHp;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
     void Update()
    {
        if(slime_hp <= 0)
        {
            dead();
        }

        time_after_attack += Time.deltaTime;
        distance_of_slime_to_player = Vector3.Distance(player.transform.position, transform.position);
        distance_of_slime_to_spawner = Vector3.Distance(spawner.transform.position, transform.position);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            Idle_act();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Chasing"))
        {
            Chase_act();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Debug.Log("6");
            Attack_act();
            
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Return"))
        {
            Chase_act();
        }
    }
    private void Chasing()
    {
        agent.isStopped = false;
        agent.stoppingDistance = 5;
        agent.SetDestination(player.transform.position);
    }

    private void Attack()
    {
        if (distance_of_slime_to_player <= attackRange)
        {
            //물슬라임의 경우 물 구체 프리팹을 발사하고 플레이어가 맞는다면 hp감소
            if (attackRange >= distance_of_slime_to_player)
            {
                Instantiate(waterBall, waterBallSpawn.transform.position, waterBallSpawn.transform.rotation);
            }
        }
        
    }

    private void dead() {
        //hp가 0일시 어떤 상태이든 isDead로
        animator.SetTrigger("isDead");
        Destroy(gameObject, death_motion_time);
    }
    private void Idle_act() {
        //대기 일경우 가능하다면 스포너 주변에 돌아다니는 기능
        agent.isStopped = true;
        if (distance_of_slime_to_player <= detectRange)
        {
            //플레이어가 감지 범위 안에 있다 
            //대기 -> 추격
            Idle_to_Chasing();
        }
    }
    private void Chase_act()
    {
        //추격상태일 경우
        Chasing();
        if (distance_of_slime_to_spawner >= limitRange || distance_of_slime_to_player > chasingRange)
        {
            //스포너와 한계이상 떨어질 경우 혹은 플레이어가 추격범위 밖일경우 
            //추격 -> 귀환
            Chasing_to_Return();
        }
        else if (distance_of_slime_to_player <= attackRange)
        {
            //플레이어가 공격범위에 들어올 경우
            //추격  -> 공격
            Chasing_to_Attack();
        }
    }
    private void Attack_act()
    {
        //공격
        //공격
        Vector3 target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(target);
        Debug.DrawRay(transform.position, transform.forward * attackRange, Color.blue, 0.3f);
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            Debug.Log(hit.transform.tag);
            if (hit.transform.CompareTag("Wall"))
            {
                agent.isStopped = false;
                agent.stoppingDistance -= 1;
                agent.SetDestination(player.transform.position);
            }
            else if (hit.transform.CompareTag("Player"))
            {
                //rb.velocity = Vector3.zero;
                //rb.angularVelocity = Vector3.zero;
                agent.isStopped = true;
                if (attackDelay < time_after_attack)
                {
                    Attack();
                    time_after_attack = 0;
                }
            }
        }


        if (distance_of_slime_to_player > attackRange && distance_of_slime_to_player <= chasingRange)
        {
            //플레이어가 공격범위 밖에 있고 플레이어가 추격 범위안에 있을 경우
            //공격 -> 추격
            Attack_to_Chasing();
        }
    }
    private void Return_act()
    {
        agent.isStopped = false;
        agent.stoppingDistance = 2;
        agent.SetDestination(spawner.transform.position);
        //귀환 구현

        if (distance_of_slime_to_spawner <= 1f)
        {
            //스포너와의 거리가 1이하 일경우 = 귀환 완료
            //귀환 -> 대기
            Return_to_Idle();
        }
        else if (distance_of_slime_to_player <= detectRange)
        {
            //귀환 도중 플레이어가 감지 범위에 들어왔을 경우
            //귀환 -> 추격
            Return_to_Chasing();
        }
    }



    void Idle_to_Chasing()
    {
        animator.SetBool("isChasing", true);
        animator.SetBool("isIdle", false);
    }
    void Return_to_Idle()
    {
        animator.SetBool("isIdle", true);
        animator.SetBool("isReturn", false);
    }
    
    void Return_to_Chasing()
    {
        animator.SetBool("isChasing", true);
        animator.SetBool("isReturn", false);
    }
    void Chasing_to_Return()
    {
        animator.SetBool("isReturn", true);
        animator.SetBool("isChasing", false);
    }
    void Chasing_to_Attack()
    {
        animator.SetBool("isAttack", true);
        animator.SetBool("isChasing", false);
    }
    void Attack_to_Chasing()
    {
        animator.SetBool("isChasing", true);
        animator.SetBool("isAttack", false);
    }
}
