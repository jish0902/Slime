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
        
        if (slime_hp <= 0)
        {
            //hp�� 0�Ͻ� � �����̵� isDead��
            animator.SetTrigger("isDead");
            Destroy(gameObject, death_motion_time);
        }

        time_after_attack += Time.deltaTime;
        distance_of_slime_to_player = Vector3.Distance(player.transform.position, transform.position);
        distance_of_slime_to_spawner = Vector3.Distance(spawner.transform.position, transform.position);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            //��� �ϰ�� �����ϴٸ� ������ �ֺ��� ���ƴٴϴ� ���
            agent.isStopped = true;
            if (distance_of_slime_to_player <= detectRange)
            {
                //�÷��̾ ���� ���� �ȿ� �ִ� 
                //��� -> �߰�
                Idle_to_Chasing();
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Chasing"))
        {
            //�߰ݻ����� ���
            Chasing();
            if (distance_of_slime_to_spawner >= limitRange || distance_of_slime_to_player > chasingRange)
            {
                //�����ʿ� �Ѱ��̻� ������ ��� Ȥ�� �÷��̾ �߰ݹ��� ���ϰ�� 
                //�߰� -> ��ȯ
                Chasing_to_Return();
            }
            else if (distance_of_slime_to_player <= attackRange)
            {
                //�÷��̾ ���ݹ����� ���� ���
                //�߰�  -> ����
                Chasing_to_Attack();
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {

            //����
            Vector3 target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(target);
            Debug.DrawRay(transform.position, transform.forward * attackRange, Color.blue, 0.3f);
            if(Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
            {
                if (hit.transform.CompareTag("Wall"))
                {
                    agent.isStopped = false;
                    agent.stoppingDistance -= 1;
                    agent.SetDestination(player.transform.position);
                }
                else if (hit.transform.CompareTag("Player")) {
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
                //�÷��̾ ���ݹ��� �ۿ� �ְ� �÷��̾ �߰� �����ȿ� ���� ���
                //���� -> �߰�
                Attack_to_Chasing();
            }

        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Return"))
        {
            agent.isStopped = false;
            agent.stoppingDistance = 2;
            agent.SetDestination(spawner.transform.position);
            //��ȯ ����

            if (distance_of_slime_to_spawner <= 1f)
            {
                //�����ʿ��� �Ÿ��� 1���� �ϰ�� = ��ȯ �Ϸ�
                //��ȯ -> ���
                Return_to_Idle();
            }
            else if (distance_of_slime_to_player <= detectRange)
            {
                //��ȯ ���� �÷��̾ ���� ������ ������ ���
                //��ȯ -> �߰�
                Return_to_Chasing();
            }
        }
    }
    private void Chasing()
    {
        agent.isStopped = false;
        agent.stoppingDistance = 10;
        agent.SetDestination(player.transform.position);
    }

    private void Attack()
    {
        //���������� ��� �� ��ü �������� �߻��ϰ� �÷��̾ �´´ٸ� hp����
        if(attackRange >= distance_of_slime_to_player)
        {
            Instantiate(waterBall, waterBallSpawn.transform.position, waterBallSpawn.transform.rotation);
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
