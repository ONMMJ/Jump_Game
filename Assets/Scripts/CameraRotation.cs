using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] Transform body;            // ��� ��ü.
    [SerializeField] Transform camPivot;        // ī�޶� ���� ��.
    [SerializeField] Transform cam;             // ī�޶�.
    [SerializeField] LayerMask layerMask;

    [Range(0.5f, 3.0f)]
    [SerializeField] float sencitivityX;        // ���� ����.
    [Range(0.5f, 3.0f)]
    [SerializeField] float sencitivityY;        // ���� ����.

    float rotateX;
    float distance;


    private void Start()
    {
        // None : ���.
        // Locked : ���콺�� �߾����� �����ȴ�.
        // Confined : ���콺 ���α�.
        //Cursor.lockState = CursorLockMode.Locked;
        distance = 10f;
    }

    private void Update()
    {
        Vector3 camBottomPos = cam.position;
        camBottomPos.y -= 0.5f;
        Vector3 camBottomDir = (camBottomPos - camPivot.position).normalized;
        Ray ray = new Ray(camPivot.position, camBottomDir);
        RaycastHit hit;
        float newDistance;
        if (Physics.Raycast(ray, out hit, distance, layerMask, QueryTriggerInteraction.Ignore))
        {
            Debug.Log(hit.transform.name);
            newDistance = Vector3.Distance(hit.point, camPivot.position);
            newDistance = Mathf.Clamp(newDistance - 0.5f, 1f, 10f);
        }
        else
        {
            newDistance = distance;
        }
        Vector3 direction = (cam.position - camPivot.position).normalized;
        cam.position = camPivot.position + (direction * newDistance);
    }

    public void RotateHorizontal(float x)
    {
        body.Rotate(Vector3.up * x * sencitivityX);
    }
    public void RotateVertical(float y)
    {
        rotateX -= (y * sencitivityY);
        rotateX = Mathf.Clamp(rotateX, -40f, 50f);
        camPivot.rotation = Quaternion.Euler(rotateX, camPivot.eulerAngles.y, 0);
    }
    public void CameraZoom(float zoom)
    {
        if (zoom == 0f)
            return;

        // zoom���� �۱� ������ ����.
        zoom = (zoom < 0) ? -1f : 1f;

        distance = Mathf.Clamp(distance - zoom, 2f, 10f);
    }

    private void OnDrawGizmos()
    {
        Vector3 camBottomPos = cam.position;
        camBottomPos.y -= 0.5f;
        Vector3 direction = (camBottomPos - camPivot.position).normalized;
        //Vector3 direction = (cam.position - camPivot.position).normalized;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(camPivot.position, direction * distance);
    }
}
