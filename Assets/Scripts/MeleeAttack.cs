using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] Vector3 attackOffset;
    [SerializeField] Animator anim;
    [SerializeField] float attackRange;
    [SerializeField] float attackRate;
    [SerializeField] float attackPower;
    [SerializeField] LayerMask attackMask;

    Collider meleeCollider;
    float rateTime;

    private void Start()
    {
        meleeCollider = GetComponent<Collider>();
    }
    private void Update()
    {
        rateTime = Mathf.Clamp(rateTime + Time.deltaTime, 0f, attackRate);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Status target = collision.gameObject.GetComponent<Status>();

        // �浹ü���� Status�� ���ų� attackMask�� layer�� ���ԵǾ����� ���� ���.
        // &�� And�������� ���� 0�̶�� ���� layerMask�� ���̾ ���ԵǾ����� �ʴٴ� ��.
        if (target == null || (target.gameObject.layer & attackMask) == 0)
            return;

        // ���� ��󿡰� attackPower�� ������ �� ���� ������ ����.
        Vector3 hitPoint = collision.contacts[0].point;

        // �浹�� ������ ���� ������ ǥ��.
        float damage = target.TakeDamage(attackPower);
        DamageEffectUI.Instance.ShowDamage(hitPoint, damage);
    }

    public void OnAttack()
    {
        // ���ݼӵ���ŭ �ð��� �帣�� �ʾҴٸ� ������ �� ����.
        if (rateTime < attackRate)
            return;

        rateTime = 0f;

        // �ִϸ��̼� ���� ó��
        anim.SetLayerWeight(1, 1f);
        anim.SetTrigger("onAttack");
    }
    /*private void OnCheckAttack()
    {
        // ���� ���� ó��
        Collider[] targets = Physics.OverlapSphere(transform.position + transform.TransformDirection(attackOffset), attackRange, attackMask);
        for (int i = 0; i < targets.Length; i++)
        {
            Status target = targets[i].GetComponent<Status>();
            if (target == null || target.gameObject == gameObject)
                continue;
            
            target.TakeDamage(attackPower);            
        }
    }*/
    private void OnStartAttack()
    {
        meleeCollider.enabled = true;
    }
    private void OnEndAttack()
    {
        meleeCollider.enabled = false;
    }
    private void OnEndUpperMask()
    {
        anim.SetLayerWeight(1, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(attackOffset), attackRange);
    }
}
