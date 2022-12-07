using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class Events : MonoBehaviour
{
    public Spawner _spawner;
    public TextMeshProUGUI StartBallCountText;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI BallPriceText;
    public TextMeshProUGUI BallScalePriceText;
    public Button StartNewGameBtn;
    public Button DropNewBallBtn;
    public GameObject LeftWall;
    public GameObject RightWall;
    public GameObject BackWall;
    public GameObject FrontWall;


    public int StartBallCount = 5;     //Default Starting amount
    public bool debugging = false;
    public bool NewGame = false;
    public float interval = 10.0f;      //how often to call the function, in seconds 
    public float nextTime = 10.0f;      //the time of the next call
    // public float ballScale = 1.0f;
    private float money = 0;
    private int ballsUsed = 0;
    private int curHighScore;
    
    void Start()
    {
        //Reset timing
        nextTime = Time.time + interval;
        updateBallsUsed();

        _spawner.SpawnBall();
        _spawner.SpawnBall();
        _spawner.SpawnBall();

        if(debugging == true){
            debug();
        }
        else{
            LeftWall.gameObject.SetActive(false);
            RightWall.gameObject.SetActive(false);
            BackWall.gameObject.SetActive(false);
            FrontWall.gameObject.SetActive(false);

        }

        if(PlayerPrefs.HasKey("GameIsSet") == false){
            Debug.Log("Game has not been initialized. Setting it now");
            PlayerPrefs.SetInt("GameIsSet", 1);
            PlayerPrefs.SetFloat("BallPrice", 2);
            PlayerPrefs.SetFloat("BallPricePrev", 1);
            PlayerPrefs.SetFloat("BallScalePrice", 2);
            PlayerPrefs.SetFloat("BallScale", 1);
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.SetInt("StartBallCount",5);
        }

        //Set initial display info
        // ballScale = PlayerPrefs.GetFloat("BallScale");
        StartBallCount = PlayerPrefs.GetInt("StartBallCount");
        updateBallsUsed();
        money = PlayerPrefs.GetInt("Money");
        float ballPrice = PlayerPrefs.GetFloat("BallPrice");
        BallPriceText.text = "$" + ballPrice;
        float ballScalePrice = PlayerPrefs.GetFloat("BallScalePrice");
        BallScalePriceText.text = "$" + ballScalePrice;
        curHighScore = PlayerPrefs.GetInt("HighScore");
        HighScoreText.text ="High Score\n " + curHighScore.ToString("0");
        
    }

    

    // Update is called once per frame
    void Update()
    {
        //If there are still more balls the user has to spawn // AND //Not at a game reset (waiting for the screen to be pressed to initiate a new game)
        if(ballsUsed < StartBallCount && NewGame == false){
            //Spawns a ball every 'interval' seconds
            if(Time.time > nextTime){
                _spawner.SpawnBall();
                // Debug.Log("Delay Spawn");
                ballsUsed += 1;
                nextTime = Time.time + interval;
                updateBallsUsed();  
            }   
        } 
        else{
                // Debug.Log("All balls used");
            }
         

        
        
        money = PlayerPrefs.GetFloat("Money");
        MoneyText.text = "$" + money.ToString("0.00");

    }

    public void Reset(float Score){

        
        curHighScore = PlayerPrefs.GetInt("HighScore");
        if(Score > curHighScore ){
            Debug.Log("New High Score! Prev: " + curHighScore + " New: " + Score);
            PlayerPrefs.SetInt("HighScore", (int)Score);
            HighScoreText.text ="High Score\n " + Score.ToString("0");
        }
        ballsUsed = 0;
        updateBallsUsed();
        NewGame = true;
    }

    public void StartNewGame(){
        Start();
        StartNewGameBtn.gameObject.SetActive(false);
        DropNewBallBtn.gameObject.SetActive(true);
        NewGame = false;
    }

    public void AddBallOnTap(){
        Debug.Log("Balls used: " + ballsUsed);
        if(ballsUsed < StartBallCount){
            _spawner.SpawnBall();
            ballsUsed+=1;
            updateBallsUsed();
        }
    }

    public void RESETCACHEDATA(){
        PlayerPrefs.DeleteKey("GameIsSet");
        PlayerPrefs.SetInt("HighScore", 0);
        HighScoreText.text ="High Score\n " + "0";
        PlayerPrefs.SetFloat("Money", 10000);
        PlayerPrefs.SetFloat("BallPrice",2);
        PlayerPrefs.SetFloat("BallPricePrev", 1);
        PlayerPrefs.SetFloat("BallScalePrice",2);
        PlayerPrefs.SetFloat("BallScale", 1);
        PlayerPrefs.SetInt("StartBallCount", 5);
        StartBallCount = 5;
        Start();

    }

    //Not Used Yet
    private void callSpawner(){
        _spawner.SpawnBall();
        Debug.Log("Calling Spawner...");
    }

    private void debug(){
        LeftWall.gameObject.SetActive(true);
        RightWall.gameObject.SetActive(true);
        BackWall.gameObject.SetActive(true);
        FrontWall.gameObject.SetActive(true);

    }

    public void updateBallsUsed(){
        StartBallCountText.text ="Balls \n" + (StartBallCount - ballsUsed);
        
    }
}
