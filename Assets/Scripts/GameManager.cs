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
    public GameObject PassedWalkers;
    public Canvas cnv;
    Text ScoreText;
    Text TempScoreText;
    GameObject PlusCollectable;
    GameObject MinusCollectable;
    GameObject EmptyCollectable;
    GameObject CurrenWalker;
    Vector3 WalkerStartPoint=new Vector3(18, 2, -21);
    Vector3 WalkerEndPoint=new Vector3(15, 2, -14);
    Vector3 PassedPoint=new Vector3(13, 2, -6);
    Vector3 PendingWalkerPos;
    Vector3 PendingWalker2Pos;
    bool WalkStarted=false;
    int score=0;
    int tempScore=0;
    float flowDice=0f;
    
    void Start()
    {
        ScoreText=cnv.transform.GetChild(0).GetComponent<Text>();
        TempScoreText=cnv.transform.GetChild(1).GetComponent<Text>();

        CurrenWalker=Walkers.transform.GetChild(0).gameObject;

        PendingWalkerPos = new Vector3(WalkerStartPoint.x+2, 2, WalkerStartPoint.z);
        PendingWalker2Pos=new Vector3(PendingWalkerPos.x+2, 2, WalkerStartPoint.z);

        PlusCollectable=Collectables.transform.GetChild(0).gameObject;
        MinusCollectable=Collectables.transform.GetChild(1).gameObject;
        EmptyCollectable=Collectables.transform.GetChild(2).gameObject;

        score=0;
        tempScore=0;
        StartCoroutine(CreateAndPlaceCollectable());        
    }
    
    void FixedUpdate()
    {        
        //Set CurrentWalker position to the WalkerEndPoint via LeftClick or Touch
        if(Input.GetMouseButton(0)){
            Debug.Log("clicked");
            CurrenWalker.transform.position=Vector3.MoveTowards(CurrenWalker.transform.position, WalkerEndPoint, WalkerSpeed);
        }

        //WalkerArrived function is triggered when CurrentWalker is ALMOST arrive to the WalkerEndPoint
        if(CurrenWalker.transform.localPosition.z == WalkerEndPoint.z){
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

        //Flowing collectables on the band
        Collectables.transform.position=Vector3.MoveTowards(Collectables.transform.position, Collectables.transform.position + new Vector3(5,0,0), WalkerSpeed);

        if(PassedWalkers.transform.childCount>0){
            for(int i=0; i<PassedWalkers.transform.childCount; i++){
                PassedWalkers.transform.GetChild(i).transform.position = Vector3.MoveTowards(PassedWalkers.transform.GetChild(i).transform.position, PassedPoint - new Vector3(i*2, 0, 0), WalkerSpeed);
            }
        }

    }

    //Set the pending walkers's positions
    void SetWaitingWalkers()
    {
        Walkers.transform.GetChild(1).transform.position=Vector3.MoveTowards(Walkers.transform.GetChild(1).transform.position, WalkerStartPoint, WalkerSpeed);
        Walkers.transform.GetChild(2).transform.position=Vector3.MoveTowards(Walkers.transform.GetChild(2).transform.position, PendingWalkerPos, WalkerSpeed);;
    }

    //Creates new pending walker, changes current walker, imports temp score to score and sets score texts
    void WalkerArrived(){

        //Creates 2nd pending walker
        Instantiate(CurrenWalker, PendingWalker2Pos, CurrenWalker.transform.rotation, Walkers.transform);


        //Set the walker as passed and set the next walker as CurrentWalker
        if(tempScore>=3){
            CurrenWalker.transform.SetParent(PassedWalkers.transform);
            CurrenWalker = Walkers.transform.GetChild(0).gameObject;
        }

        //Destroy the walker and set the next walker as CurrentWalker
        else if(tempScore<3){
            CurrenWalker = Walkers.transform.GetChild(1).gameObject;
            Destroy(Walkers.transform.GetChild(0).gameObject);
        }
        
        
        WalkStarted=false;

        

        //Import walker score to the general score and set tempScore to 0
        score=score+tempScore;
        tempScore=0;

        //Set score texts
        ScoreText.text=score.ToString();
        TempScoreText.text=tempScore.ToString();

        //Destroy the arrived walker
        Debug.Log("Walker has arrived!");
    }

    //Creates random collectables in a row
    IEnumerator CreateAndPlaceCollectable(){

        Instantiate(RandomItemCreator(), new Vector3(8,1,-18.5f), PlusCollectable.transform.rotation, Collectables.transform);
        Instantiate(RandomItemCreator(), new Vector3(8,1,-17), PlusCollectable.transform.rotation, Collectables.transform);
        Instantiate(RandomItemCreator(), new Vector3(8,1,-15.5f), PlusCollectable.transform.rotation, Collectables.transform);

        yield return new WaitForSeconds(DeltaFlow);
        StartCoroutine(CreateAndPlaceCollectable());
    }

    //Returns random collectable
    GameObject RandomItemCreator(){
        flowDice=UnityEngine.Random.Range(0f, 3f);

        if(flowDice<=1){
            return MinusCollectable;
        }

        else if(flowDice>1 && flowDice<=2){
            return PlusCollectable;
        }

        else{
            return EmptyCollectable;
        }

    }
    
    //Increases Temp Score
    public void IncTempScore(){
        tempScore++;
        TempScoreText.text=tempScore.ToString();
    }

    //Decreases Temp Score
    public void DecTempScore(){
        tempScore--;
        TempScoreText.text=tempScore.ToString();
    }

    //Reload the current scene
    public void Replay(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
