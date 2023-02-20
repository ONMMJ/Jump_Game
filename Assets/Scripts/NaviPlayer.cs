using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NaviPlayer : MonoBehaviour
{
    enum STATE
    {
        Idle,           // ������ �ִ�.
        MoveOnly,       // �̵��� �ϴ� ��.
        MoveAttack,     // �̵��ϸ鼭 ����.
        Targetting,     // ��Ȯ�ϰ� Ư���� ���� Ÿ����.
    }

    [SerializeField] float attackRange;

    NavMeshAgent agent;

    STATE state;            // ����.
    Vector3 destination;    // ������.
    Transform target;       // ���� ���.
    bool isAttackCommand;   // ���� ���.

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            isAttackCommand = true;

        if (Input.GetMouseButtonDown(0) && isAttackCommand)
        {
            RaycastHit hit = GetRayPoint();
            destination = hit.point;

            // hit������� Enemy���� �Ǵ�.
            int targetLayer = 1 << hit.collider.gameObject.layer;
            int enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
            if ((targetLayer & enemyLayer) > 0)
            {
                state = STATE.Targetting;
                target = hit.transform;
            }
            else
            {
                state = STATE.MoveAttack;
                target = null;
            }

            isAttackCommand = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit = GetRayPoint();
            destination = hit.point;
            isAttackCommand = false;
            target = null;

            state = STATE.MoveOnly;
        }

        switch (state)
        {
            case STATE.Idle:
                Debug.Log("Idle");
                break;
            case STATE.MoveOnly:
                OnMoveOnly();
                Debug.Log("�̵�");
                break;
            case STATE.MoveAttack:
                OnMoveAttack();
                Debug.Log("���� �̵�");
                break;
            case STATE.Targetting:
                Debug.Log("Ÿ�� ����");
                break;
        }
    }

    private void OnMoveOnly()
    {
        agent.SetDestination(destination);      // ������ ����.
        Debug.Log(agent.remainingDistance);
        if (agent.remainingDistance <= 0)       // ���������� ���� �Ÿ� 0���� �� ��
            state = STATE.Idle;                 // ���� ����.
    }
    private void OnMoveAttack()
    {
        agent.SetDestination(destination);      // ������ ����.
        if (Physics.CheckSphere(transform.position, attackRange, 1 << LayerMask.NameToLayer("Enemy")))
        {
            state = STATE.Targetting;
            // target = 
        }
        else if (agent.remainingDistance <= 0)  // ���������� ���� �Ÿ� 0���� �� ��
            state = STATE.Idle;                 // ���� ����.
    }

    private RaycastHit GetRayPoint()
    {
        // ���콺 ������ ��ġ�� ray�� �����Ѵ�.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, float.MaxValue);
        return hit;
    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, attackRange);
    }
}
