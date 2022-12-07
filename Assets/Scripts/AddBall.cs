using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class AddBall : MonoBehaviour
{
    public Events _events;
    public TextMeshProUGUI BallPriceText;
    private float money;
    private float ballPrice;
    public void ButtonClicked(){
        if(_events.debugging == true){
            Debug.Log("Baught a ball");
        }

        ballPrice = PlayerPrefs.GetFloat("BallPrice");
        money = PlayerPrefs.GetFloat("Money");
        Debug.Log("Ball cost: " + ballPrice);

        //BUY
        if(money >= ballPrice){
            _events.StartBallCount += 1;
           
            //Calculate cost of this purchase & next 
            PlayerPrefs.SetFloat("Money", money - ballPrice);
            float ballPricePrev = PlayerPrefs.GetFloat("BallPricePrev");
            float tmpBallPrice = ballPrice;
            ballPrice = ballPrice + ballPricePrev;

            PlayerPrefs.SetFloat("BallPrice", ballPrice);
            PlayerPrefs.SetFloat("BallPricePrev",tmpBallPrice);

            BallPriceText.text = "$" + ballPrice;

            PlayerPrefs.SetInt("StartBallCount", _events.StartBallCount);
            _events.updateBallsUsed();
        }
        else{
            Debug.Log("Not enough money");
        }



       
    }

}
