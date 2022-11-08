using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float WalkerSpeed;
    public float DeltaFlow;
    public GameObject Walkers;
    public GameObject Collectables;
    GameObject PlusCollectable;
    GameObject MinusCollectable;
    GameObject CurrenWalker;
    Vector3 WalkerStartPoint=new Vector3(18, 2, -21);
    Vector3 WalkerEndPoint=new Vector3(15, 2, -14);
    Vector3 PendingWalkerPos;
    Vector3 PendingWalker2Pos;
    bool WalkStarted=false;
    int score=0;
    int tempScore=0;
    float flowDice=0f;


    
    void Start()
    {
        CurrenWalker=Walkers.transform.GetChild(0).gameObject;

        PendingWalkerPos = new Vector3(WalkerStartPoint.x+2, 2, WalkerStartPoint.z);
        PendingWalker2Pos=new Vector3(PendingWalkerPos.x+2, 2, WalkerStartPoint.z);

        PlusCollectable=Collectables.transform.GetChild(0).gameObject;
        MinusCollectable=Collectables.transform.GetChild(1).gameObject;

        score=0;
        tempScore=0;
        StartCoroutine(ItemFlower());        
    }
    
    void FixedUpdate()
    {
        //Set CurrentWalker position to the WalkerEndPoint via LeftClick or Touch
        if(Input.GetMouseButton(0)){
            Debug.Log("clicked");
            CurrenWalker.transform.position=Vector3.MoveTowards(CurrenWalker.transform.position, WalkerEndPoint, WalkerSpeed);
        }

        //WalkerArrived function is triggered when CurrentWalker is ALMOST arrive to the WalkerEndPoint
        if(CurrenWalker.transform.localPosition.z+0.2 >= WalkerEndPoint.z){
            WalkerArrived();
        }

        //WalkStarted is true if CurrentWalker is on the band
        WalkStarted = (CurrenWalker.transform.position.z>WalkerStartPoint.z+2 && !WalkStarted) ? true : false;
        if(WalkStarted){
            SetWaitingWalkers();
        }

        //Press "R" for reload the current scene
        if(Input.GetKeyDown(KeyCode.R)){
            Replay();
        }

    }

    //Set the pending walkers's positions
    void SetWaitingWalkers()
    {
        Walkers.transform.GetChild(1).transform.position=Vector3.MoveTowards(Walkers.transform.GetChild(1).transform.position, WalkerStartPoint, WalkerSpeed);
        Walkers.transform.GetChild(2).transform.position=Vector3.MoveTowards(Walkers.transform.GetChild(2).transform.position, PendingWalkerPos, WalkerSpeed);;
    }


    void WalkerArrived(){

        //Creates 2nd pending walker
        Instantiate(CurrenWalker, PendingWalker2Pos, CurrenWalker.transform.rotation, Walkers.transform);

        //Set CurrentWalker to next walker
        CurrenWalker=Walkers.transform.GetChild(1).gameObject;
        WalkStarted=false;

        //Destroy the arrived walker
        Destroy(Walkers.transform.GetChild(0).gameObject);
        Debug.Log("Walker has arrived!");
    }

    IEnumerator ItemFlower(){
        Debug.Log("Created");

        Instantiate(PlusCollectable, new Vector3(8,2,-18), PlusCollectable.transform.rotation, Collectables.transform);
        Instantiate(PlusCollectable, new Vector3(8,2,-17), PlusCollectable.transform.rotation, Collectables.transform);
        Instantiate(PlusCollectable, new Vector3(8,2,-16), PlusCollectable.transform.rotation, Collectables.transform);

        yield return new WaitForSeconds(DeltaFlow);
        StartCoroutine(ItemFlower());
    }

    void RandomItemCreator(){
        flowDice=Mathf.RoundToInt(UnityEngine.Random.Range(-18f,-16f));
    }
    

    //Reload the current scene
    public void Replay(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
