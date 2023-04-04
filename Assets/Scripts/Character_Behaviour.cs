using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Behaviour : MonoBehaviour
{

    [SerializeField] private CapsuleCollider Collider;

    void Start()
    {
        Collider = GetComponentInChildren<CapsuleCollider>();
    }

    private void OnCollisionEnter(Collision collider)
        {
            Debug.Log("Collided Object is = " + collider.gameObject.name);
        }

    






}