using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] float GravityRadius = 15f;
    void Start()
    {

    }

    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(transform.position, GravityRadius);
    }
}
