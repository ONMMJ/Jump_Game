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
    bool isLockControl;     // 플레이어 제어 불가능.

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
        // 이동.
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        isMove = x != 0f || y != 0f;

        Vector3 forward = transform.forward * y;        // 내 기준 앞쪽으로.
        Vector3 right = transform.right * x;            // 내 기준 오른쪽으로.
        Vector3 dir = (forward + right).normalized;     // 두 벡터를 더한 후 정규화.

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

        // 점프.
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
        // 수평 회전.
        float mouseX = Input.GetAxis("Mouse X");
        rotation.RotateHorizontal(mouseX);

        // 수직 회전.
        float mouseY = Input.GetAxis("Mouse Y");
        rotation.RotateVertical(mouseY);

        // 카메라 줌.
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        rotation.CameraZoom(zoom);
    }

    public void LockControl(bool isLock)
    {
        isLockControl = isLock;
    }
}