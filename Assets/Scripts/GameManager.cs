using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Vector3 WalkerStartPoint=new Vector3(18, 2, -21);
    Vector3 WalkerEndPoint=new Vector3(15, 2, -14);
    public GameObject CurrenWalker;
    public float WalkerSpeed;
    bool isArrived=false;
    
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            Replay();
        }

        if(Input.GetMouseButton(0) && !isArrived){
            Debug.Log("clicked");
            CurrenWalker.transform.position=Vector3.MoveTowards(CurrenWalker.transform.position, WalkerEndPoint, WalkerSpeed);
        }
            
        if(CurrenWalker.transform.localPosition.z+0.2 >= WalkerEndPoint.z && !isArrived){
            WalkerArrived();
        }

        
        
    }

    void WalkerArrived(){
        isArrived=true;
        Debug.Log("Walker has arrived!");
    }
    public void Replay(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
