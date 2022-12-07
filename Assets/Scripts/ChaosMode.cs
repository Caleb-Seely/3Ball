using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosMode : MonoBehaviour
{
    public Spawner _spawner;
    public bool enableChaos = false;
    void Update()
    {
        if(enableChaos){
            chaos();
            callSpawner();
        }        
    }

    private void chaos(){
        //Fucking chaos mode 
        InvokeRepeating ("callSpawner", 1, 1);

    }

    public void  swapChaos(){
        if(enableChaos){
            enableChaos = false;
            CancelInvoke();
        }else{
            enableChaos = true;
        }
    }

    private void callSpawner(){
        _spawner.SpawnBall();
    }   
}
