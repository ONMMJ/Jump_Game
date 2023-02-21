using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseArea : MonoBehaviour
{
    public static ChaseArea instance { get; private set; }

    public bool isChase;

    private void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            isChase = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            isChase = false;
    }
}
