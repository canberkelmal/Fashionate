using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remover : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other){
        Debug.Log("Removed");
        Destroy(other.gameObject);
    }
    
}
