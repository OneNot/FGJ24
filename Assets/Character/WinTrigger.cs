using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] InGameUIManager UIManager;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            UIManager.GameOver(true);
        }
    }
}
