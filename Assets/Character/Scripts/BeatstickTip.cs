using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatstickTip : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D other) {
        if(true) 
        {
            playerMovement.WeaponTrigger();
            //other.attachedRigidbody.gameObject.SetActive(false);
        }
    }
}
