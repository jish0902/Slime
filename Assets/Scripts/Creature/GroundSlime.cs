using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundSlime : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;

    GameObject player;
    public GameObject spawner;

    public int slime_hp;
    int slime_maxHp = 3;
    float distance_of_slime_to_player;
    float distance_of_slime_to_spawner;
    float attackRange = 8;
    float chasingRange = 15;
    float detectRange = 7;
    float limitRange = 20;
    float attackDelay = 7;
    float time_after_attack = 0;
    float death_motion_time = 2;


    // Start is called before the first frame update
    void Awake()
    {
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
            if (attackDelay < time_after_attack)
            {

                Debug.Log("Attack!!");
                Attack();
                time_after_attack = 0;
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
        agent.stoppingDistance = 5;
        agent.SetDestination(player.transform.position);
    }

    private void Attack()
    {
        //���������� ��� ��ä�� ���� ��ä�� ������� ����� �߻� �÷��̾ ����Ŀ� ������ hp����

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
