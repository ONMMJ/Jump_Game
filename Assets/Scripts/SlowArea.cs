using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowArea : MonoBehaviour
{
    [SerializeField] float slowRate;

    private void OnTriggerEnter(Collider other)
    {
        Movement3D movement = other.GetComponent<Movement3D>();
        if(movement != null)
        {
            movement.SlowDebuff(slowRate);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Movement3D movement = other.GetComponent<Movement3D>();
        if (movement != null)
        {
            movement.SlowDebuff(1f);
        }
    }
}
