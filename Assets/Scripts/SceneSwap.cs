using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwap : MonoBehaviour
{
    public void GoToGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void GoToMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
    
}
