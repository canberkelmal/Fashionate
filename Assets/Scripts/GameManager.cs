using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Walkers;
    public float WalkerSpeed;
    GameObject CurrenWalker;
    Vector3 WalkerStartPoint=new Vector3(18, 2, -21);
    Vector3 WalkerEndPoint=new Vector3(15, 2, -14);
    Vector3 PendingWalkerPos;
    Vector3 PendingWalker2Pos;
    bool WalkStarted=false;
    
    void Start()
    {
        CurrenWalker=Walkers.transform.GetChild(0).gameObject;
        PendingWalkerPos = new Vector3(WalkerStartPoint.x+2, 2, WalkerStartPoint.z);
        PendingWalker2Pos=new Vector3(PendingWalkerPos.x+2, 2, WalkerStartPoint.z);
    }
    
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            Replay();
        }

        if(Input.GetMouseButton(0)){
            Debug.Log("clicked");
            CurrenWalker.transform.position=Vector3.MoveTowards(CurrenWalker.transform.position, WalkerEndPoint, WalkerSpeed);
        }
            
        if(CurrenWalker.transform.localPosition.z+0.2 >= WalkerEndPoint.z){
            WalkerArrived();
        }

        if(CurrenWalker.transform.position.z>WalkerStartPoint.z+2 && !WalkStarted){
            WalkStarted=true;
        }

        if(WalkStarted){
            SetWaitingWalkers();
        }
    }

    void SetWaitingWalkers()
    {
        Walkers.transform.GetChild(1).transform.position=Vector3.MoveTowards(Walkers.transform.GetChild(1).transform.position, WalkerStartPoint, WalkerSpeed);
        Walkers.transform.GetChild(2).transform.position=Vector3.MoveTowards(Walkers.transform.GetChild(2).transform.position, PendingWalkerPos, WalkerSpeed);;
    }

    void WalkerArrived(){
        Instantiate(CurrenWalker, PendingWalker2Pos, CurrenWalker.transform.rotation, Walkers.transform);
        CurrenWalker=Walkers.transform.GetChild(1).gameObject;
        Destroy(Walkers.transform.GetChild(0).gameObject);
        Debug.Log("Walker has arrived!");
    }

    public void Replay(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
