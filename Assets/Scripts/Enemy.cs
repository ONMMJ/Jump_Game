using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public enum STATE
    {
        Idle,
        Patrol,
        Chase,
        Attack,
    }

    [SerializeField] new string name;
    [SerializeField] Transform uiPivot;     // ui�� ��µ� ������.

    [Header("Event")]
    [SerializeField] UnityEvent onDead;     // ���� �׾��� �� ȣ��Ǵ� �̺�Ʈ �Լ�.

    [Header("Enemy")]
    [SerializeField] STATE state;
    [SerializeField] float patrolRadius;    // ���� ����.
    [SerializeField] float searchRadius;    // Ž�� ����.
    [SerializeField] float attackRadius;    // ���� ����.

    [Header("Movement")]
    [SerializeField] float moveSpeed;       // �̵� �ӵ�.
    [SerializeField] float attackRate;      // ���� �ֱ�.

    public string Name => name;
    //public Transform transform => uiPivot;

    bool isChaseToPlayer;       // �÷��̾ �߰��ϴ°�.
    bool isAttackToPlayer;      // �÷��̾ �����ϴ°�.
    bool isChangedState;        // ���°� ���ߴ°�.
    
    float attackTime;           // ���� �ð�.
    float idleTime = 0f;        // ��� �ð�.
    Vector3 originPoint;        // ���� ��Ȱ ����.
    Vector3 patrolPoint;        // ���� ����.

    STATE beforeState;


    private void Start()
    {
        idleTime = Random.Range(2f, 5f);
        originPoint = transform.position;
        patrolPoint = transform.position;
    }
    private void Update()
    {
        isChaseToPlayer = Physics.CheckSphere(transform.position, searchRadius, 1 << LayerMask.NameToLayer("Player"));
        isAttackToPlayer = Physics.CheckSphere(transform.position, attackRadius, 1 << LayerMask.NameToLayer("Player"));

        // ���¸ӽ�.
        if (isChaseToPlayer && !isAttackToPlayer)
            state = STATE.Chase;
        else if (isChaseToPlayer && isAttackToPlayer)
            state = STATE.Attack;
        else if (Vector3.Distance(transform.position, patrolPoint) > 0)
            state = STATE.Patrol;
        else
            state = STATE.Idle;

        isChangedState = beforeState != state;      // ���� ���¿� ���� ���°� �ٸ� ���.
        beforeState = state;                        // ���� ���������� �Ѿ�� ���� ���� ���� ����.

        switch (state)
        {
            case STATE.Idle: OnIdle(); break;
            case STATE.Chase: OnChase(); break;
            case STATE.Patrol: OnPatrol(); break;
            case STATE.Attack: OnAttack(); break;
        }
    }

    private void OnIdle()
    {
        // ���� ��ȭ�� ���� ��� ���°� �Ǿ��� ���.
        if (isChangedState)
            idleTime = Random.Range(2f, 5f);

        // 3�ʰ� ����ϰ� �ִٰ� �����Ѵ�.
        idleTime -= Time.deltaTime;
        if (idleTime <= 0f)
        {
            Vector2 randomUnitCircle = Random.insideUnitCircle * patrolRadius;
            patrolPoint = originPoint + new Vector3(randomUnitCircle.x, 0f, randomUnitCircle.y);
        }
    }
    private void OnPatrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoint, moveSpeed * Time.deltaTime);
    }
    private void OnChase()
    {
        Vector3 playerPos = Player.Instance.transform.position;
        playerPos.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, playerPos, moveSpeed * Time.deltaTime);
    }
    private void OnAttack()
    {
        // ���� ���¿��� �������� ��ȯ�� Ÿ�̹�.
        if(isChangedState)
        {
            attackTime = attackRate;
        }

        attackTime -= Time.deltaTime;
        if (attackTime <= 0f)
        {
            attackTime = attackRate;
            Debug.Log("�÷��̾� ����");
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ���� ����.
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(originPoint, Vector3.up, patrolRadius);

        // Ž�� ����.
        UnityEditor.Handles.color = Color.cyan;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, searchRadius);

        // ���� ����.
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, attackRadius);
    }
}
