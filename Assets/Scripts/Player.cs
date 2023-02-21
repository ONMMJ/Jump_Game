using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] Transform body;

    CameraRotation rotation;
    Movement3D movement;
    Animator anim;

    bool isMove;
    bool isJump;
    bool isSlide;
    bool isLockControl;     // �÷��̾� ���� �Ұ���.

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        rotation = GetComponent<CameraRotation>();
        movement = GetComponent<Movement3D>();
        anim = body.GetComponent<Animator>();

        isLockControl = true;
    }
    private void Update()
    {
        if (!isLockControl)
        {
            Movement();
            Rotate();
        }
    }
    private void LateUpdate()
    {
        anim.SetBool("isMove", isMove);
        anim.SetBool("isJump", isJump);
        anim.SetBool("isSlide", isSlide);
    }

    private void Movement()
    {
        // �̵�.
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        isMove = x != 0f || y != 0f;

        Vector3 forward = transform.forward * y;        // �� ���� ��������.
        Vector3 right = transform.right * x;            // �� ���� ����������.
        Vector3 dir = (forward + right).normalized;     // �� ���͸� ���� �� ����ȭ.

        if (!isSlide)
        {
            movement.Move(dir);
        }
        if (isMove)
        {
            body.rotation = Quaternion.Slerp(body.rotation, Quaternion.LookRotation(dir), 0.1f);
        }

        if (movement.IsRending)
        {
            isSlide = false;
            isJump = false;
        }
        else
        {
            if (isSlide)
                isJump = false;
        }

        // ����.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (movement.IsGround && !isSlide)
            {
                isJump = true;
                Sound.instance.PlayJump();
            }
            movement.Jump();
        }

        if (isJump && Input.GetKeyDown(KeyCode.E))
        {
            isSlide = true;
            Sound.instance.PlayJump();
            movement.Slide(body);
        }
    }
    private void Rotate()
    {
        // ���� ȸ��.
        float mouseX = Input.GetAxis("Mouse X");
        rotation.RotateHorizontal(mouseX);

        // ���� ȸ��.
        float mouseY = Input.GetAxis("Mouse Y");
        rotation.RotateVertical(mouseY);

        // ī�޶� ��.
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        rotation.CameraZoom(zoom);
    }

    public void LockControl(bool isLock)
    {
        isLockControl = isLock;
    }
}