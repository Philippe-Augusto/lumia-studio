using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lumes : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            //other.gameObject.GetComponent<Player>().AtivarPlanagem();
            Debug.Log("Lume do Ar Coletado");
            Destroy(this.gameObject);
        }
    }
}
