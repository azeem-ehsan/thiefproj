using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] private ParticleSystem DiamondParticle;
    [Header("Model")]
    [SerializeField] private GameObject DiamondMesh;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            // play VFX
            DiamondParticle.Play();
            Debug.Log("Collected a Diamond in Diamond");
            DiamondMesh.SetActive(false);

        }
    }

}
