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

    float inputX;
    float inputY;
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
    }
    private void Update()
    {/*
        inputX = 0;
        inputY = 0;
*/
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

    private void SmoothMove(float dir, ref float value)
    {
        switch (dir)
        {
            case 1:
                value = Mathf.Clamp(value + Time.deltaTime * 5, -1f, 1f);
                break;
            case 0:
                if (value > 0)
                    value = Mathf.Clamp(value - Time.deltaTime * 5, 0, 1f);
                else if (value == 0)
                    value = 0;
                else
                    value = Mathf.Clamp(value + Time.deltaTime * 5, -1f, 0);
                break;
            case -1:
                value = Mathf.Clamp(value - Time.deltaTime * 5, -1f, 1f);
                break;
            default:
                break;
        }
    }
    private void Movement()
    {
        // 이동.
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        SmoothMove(x, ref inputX);
        SmoothMove(y, ref inputY);
        isMove = x != 0f || y != 0f;

        // 2D처럼 시점이 고정된 환경이 아니라
        // 회전이 들어가는 3D에서는
        // 정면을 '절대좌표'로 잡지 말고 '로컬좌표'로 잡아야한다.
        Vector3 forward = transform.forward * y;        // 내 기준 앞쪽으로.
        Vector3 right = transform.right * x;            // 내 기준 오른쪽으로.
        Vector3 dir = (forward + right).normalized;     // 두 벡터를 더한 후 정규화.

        if(!isSlide)
            movement.Move(dir);

        // 점프.
        if (Input.GetKeyDown(KeyCode.Space))
            movement.Jump();


        if(movement.IsGround)
        {
            isSlide = false;
            isJump = false;
        }
        else
        {
            if(!isSlide)
                isJump = true;
            else
                isJump = false;
        }

        if (isJump && Input.GetKeyDown(KeyCode.E))
        {
            isSlide = true;
            movement.Slide();
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
}