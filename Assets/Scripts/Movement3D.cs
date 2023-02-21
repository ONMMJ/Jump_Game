using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement3D : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float slidePower;

    [Header("Check Ground")]
    [SerializeField] float checkGroundRadius;
    [SerializeField] Vector3 checkGroundOffset;
    [SerializeField]LayerMask groundMask;               // 지면 마스크.

    Rigidbody rigid;     // 캐릭터 컨트롤러.
    float slowDebuff = 1;
    bool isGround;                      // 땅에 서 있는가?

    public bool IsRending => isGround && rigid.velocity.y < 0;
    public bool IsGround => isGround;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }   

    private void Update()
    {
        // Check Ground.
        Vector3 pivot = transform.position + checkGroundOffset;
        isGround = Physics.CheckSphere(pivot, checkGroundRadius, groundMask);
    }
    public void SlowDebuff(float value)
    {
        slowDebuff = value;
    }
    public void Move(Vector3 dir)
    {
        Vector3 velocity = dir * moveSpeed * slowDebuff;
        velocity.y = rigid.velocity.y;
        rigid.velocity = velocity;
    }
    public void Jump()
    {
        if (!isGround)
            return;
        /*
                Vector3 velocity = rigid.velocity;
                velocity.y = Mathf.Sqrt(jumpHeight);
                rigid.velocity = velocity;*/
        rigid.AddForce(Vector3.up * jumpPower * slowDebuff, ForceMode.Impulse);
    }
    public void Slide()
    {
        /*Vector3 velocity = Vector3.zero;
        velocity.y = rigid.velocity.y;
        rigid.velocity = velocity;*/
        rigid.AddForce(transform.forward * slidePower * slowDebuff, ForceMode.Impulse);
    }
    public void Slide(Transform body)
    {
        /*Vector3 velocity = Vector3.zero;
        velocity.y = rigid.velocity.y;
        rigid.velocity = velocity;*/
        rigid.AddForce(body.forward * slidePower * slowDebuff, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 pivot = transform.position + checkGroundOffset;
        Gizmos.DrawWireSphere(pivot, checkGroundRadius);
    }
}
