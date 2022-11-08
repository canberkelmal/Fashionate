using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Walker : MonoBehaviour
{
    public GameManager gm;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Minus"){
            gm.DecTempScore();
        }
        else if(other.gameObject.tag=="Plus"){
            gm.IncTempScore();
        }
        Destroy(other.gameObject);
    }
}
