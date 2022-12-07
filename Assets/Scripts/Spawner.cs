using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Circle;
    GameObject ball;
    private Vector3 scaleChange;

    public int RandomTorque = 15;
    public int RandomSpawn = 2;

    //Used for Testing
    public float spawnTime = 1f;
    public float DestroyBallTimer = 10f;
    public bool DestroyBalls = true;
     void Start () 
    {
   
        //InvokeRepeating ("SpawnBall", spawnTime, spawnTime);
        //Instantiate(Circle, transform.position,transform.rotation);
    }
    public void SpawnBall()
    {  
        float ballScale = PlayerPrefs.GetFloat("BallScale"); 
        // Debug.Log("Ball Scale:" + ballScale); 
        Vector3 randPosition = new Vector3(Random.Range(-RandomSpawn,RandomSpawn),0,Random.Range(-RandomSpawn,RandomSpawn));
        ball = Instantiate(Circle,transform.position + randPosition,transform.rotation) as GameObject;
        ball.GetComponent<Rigidbody>().AddTorque(Random.Range(-RandomTorque,RandomTorque),Random.Range(-RandomTorque,RandomTorque),0);
        ball.transform.parent = transform;
        scaleChange = new Vector3(ballScale, ballScale, ballScale);
        ball.transform.localScale = scaleChange;

        if(DestroyBalls){
            Destroy(ball,DestroyBallTimer);
        }        
    }
}
