using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    void Start(){

    }
    
    void OnTriggerEnter(Collider other){
        Debug.Log("Removed");
        Destroy(other.gameObject);
    }
    
}
