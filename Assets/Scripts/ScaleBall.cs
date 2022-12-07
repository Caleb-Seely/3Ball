using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScaleBall : MonoBehaviour
{
    public TextMeshProUGUI ScaleBallPriceText;

    public void ScaleBallOnClick(){
    
        float ballScalePrice = PlayerPrefs.GetFloat("BallScalePrice");
        float money = PlayerPrefs.GetFloat("Money");

        if( money >= ballScalePrice){
            float ballScale = PlayerPrefs.GetFloat("BallScale");
            ballScale -= .01f;
            PlayerPrefs.SetFloat("BallScale",ballScale);
            PlayerPrefs.SetFloat("Money", money - ballScalePrice);
            ballScalePrice = ballScalePrice*2;
            PlayerPrefs.SetFloat("BallScalePrice", ballScalePrice);
            ScaleBallPriceText.text = "$" + ballScalePrice; 
            Debug.Log("Fuck: "+ballScale +"\nPrice: "+ballScalePrice);
        }
        else{
            Debug.Log("Not enough money");
        }
    }
}
