using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using UnityEngine.Events;       // for using Events in Unity

public class Diamond : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] private ParticleSystem DiamondParticle;
    [Header("Model")]
    [SerializeField] private GameObject DiamondMesh;



    [SerializeField] private UnityEvent myTriggerEvent; // create an Event which will be triggered when the player enters the trigger


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

            myTriggerEvent.Invoke(); // trigger the Event

            // Play Player pickup Animation
            // Play Player pickup Sound
            // Add to Score
            // Add to Player Inventory



        }
    }

}
