using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class ScoreTimer : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public Events _events;
    public GameObject[] ballTag;
    public Button StartNewGameBtn;
    public Button DropNewBallBtn;

    private float timePassed = 0.00f;
    private int liveBalls = 0;
    public bool TestEndState = false;
    private float bank = 0;
    private string finalScore = "Zero";

      void Update()
    {

        //Find the number of balls on the screen
        ballTag = GameObject.FindGameObjectsWithTag("Ball");
        liveBalls = ballTag.Length;

        //Game Over Condition                                                                           //Game Over Conditions
        if(liveBalls < 3 && TestEndState == true){

            ScoreText.fontSize = 150;
            //If we haven't called to reset the game | (flips NewGame to True)
            if( _events.NewGame == false){
                finalScore = timePassed.ToString("0");
                ScoreText.text = finalScore + "\nGame Over!";
                _events.Reset(timePassed);
                timePassed = 0.00f;
                StartNewGameBtn.gameObject.SetActive(true);
                DropNewBallBtn.gameObject.SetActive(false);
                Debug.Log("Restart Btn is active");
            }
            
        }
        else{

            //"Score" calculatiions
            timePassed += Time.deltaTime * liveBalls;

            //Get what they prev had
            bank = PlayerPrefs.GetFloat("Money");
            // Debug.Log("Player Bank: " +bank);
            //Add it to what they earned
            //Events handles the visual update 
            bank += Time.deltaTime * liveBalls;
            PlayerPrefs.SetFloat("Money", bank);
            
            ScoreText.text = timePassed.ToString("0");
            ScoreText.fontSize = 200;
        }

        
            // Debug.Log(liveBalls.ToString());

    }
}
