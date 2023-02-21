using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{ 
    [SerializeField] float moveSpeed;       // 이동 속도.
    [SerializeField] float stopDistance;

    private void Update()
    {
        if(ChaseArea.instance.isChase) { OnChase(); }
    }
    private void OnChase()
    {
        if (Vector3.Distance(Player.Instance.transform.position, transform.position) > stopDistance)
        {
            Vector3 playerPos = Player.Instance.transform.position;
            playerPos.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, playerPos, moveSpeed * Time.deltaTime);
        }
    }
}
