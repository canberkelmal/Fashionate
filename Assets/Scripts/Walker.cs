using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Walker : MonoBehaviour
{
    public GameManager gm;

    void OnTriggerEnter(Collider other){

        //If the collectable's tag "Minus", decrease temp score
        if(other.gameObject.tag=="Minus"){
            gm.DecTempScore();
        }
        
        //If the collectable's tag "Plus", increase temp score
        else if(other.gameObject.tag=="Plus"){
            gm.IncTempScore();
        }

        //Destroy collected object
        Destroy(other.gameObject);
    }
}
