using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireSlime : Monster 
{
    
    NavMeshAgent agent;
    Animator animator;
    
    GameObject player;
    public GameObject spawner;
    
    float distance_of_slime_to_player;
    float distance_of_slime_to_spawner;

    //적 스텟
    float detectRange = 10;
    float chasingRange = 20;
    float  attackRange = 5;
    float limitRange = 20;
    float attackDelay = 5;
    float time_after_attack = 0;
    float death_motion_time =2;

    
    
    void Awake()
    {
        //반드시 추가해야함
        OnEnable();
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public override bool ApplyDamage(DamageMessage damageMessage)
    {
        //데미지 받으면 0.1초 자동 무적, health가 0일시 DIE호출
        bool ishit = base.ApplyDamage(damageMessage);
          
        return ishit;
    }


    void Update()
    {
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

    private void Attack() //변경
    {
        if (distance_of_slime_to_player <= attackRange)
        {
            //player.GetComponent<PlayerCharacter>().player_HP -= 1;

            DamageMessage dm = new DamageMessage() { amount = monsterData.damage, damager = this.gameObject, };
            player.GetComponent<PlayerCharacter>().ApplyDamage(dm);
            Debug.Log(player.GetComponent<PlayerCharacter>().health);
        }
    }
    
    public override void Die()
    {
        //hp가 0일시 어떤 상태이든 isDead로
        animator.SetTrigger("isDead");
        Destroy(gameObject, death_motion_time);
        
        base.Die();
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
        if (attackDelay < time_after_attack)
        {
            Attack();
            time_after_attack = 0;
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
